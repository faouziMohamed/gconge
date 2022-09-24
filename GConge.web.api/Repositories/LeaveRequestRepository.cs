﻿using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models;
using GConge.Models.Models.Entities;
using GConge.web.api.Data;
using GConge.web.api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GConge.web.api.Repositories;

public sealed class LeaveRequestRepository : ILeaveRequestRepository
{
  private readonly GCongeDbContext _context;
  public LeaveRequestRepository(GCongeDbContext context)
  {
    _context = context;
  }
  public async Task<LeaveRequest?> GetLeaveRequestById(int id)
  {
    return await _context.LeaveRequests
      .Include(static lvr => lvr.RequestingEmployee)
      .FirstOrDefaultAsync(x => x.Id == id);
  }
  public async Task<List<LeaveRequest>> GetLeaveRequests()
  {
    return await _context.LeaveRequests
      .Include(static lvr => lvr.RequestingEmployee)
      .ToListAsync();
  }
  public async Task<LeaveRequest> DeleteLeaveRequest(int id)
  {
    var leaveRequest = await _context.LeaveRequests.FindAsync(id);

    if (leaveRequest == null)
    {
      throw new Exception("Cannot delete a leave request that does not exist");
    }

    _context.LeaveRequests.Remove(leaveRequest);
    await _context.SaveChangesAsync();
    return leaveRequest;
  }

  public async Task<LeaveRequest?> RejectLeaveRequest(int id)
  {
    return await UpdateLeaveRequestStatus(id, LeaveRequestStatus.Rejected);
  }

  public async Task<LeaveRequest?> ApproveLeaveRequest(int id)
  {
    return await UpdateLeaveRequestStatus(id, LeaveRequestStatus.Approved);
  }

  public async Task<bool> LeaveRequestExists(int id)
  {
    return await _context.LeaveRequests.AnyAsync(x => x.Id == id);
  }
  public async Task<LeaveRequest> AddLeaveRequest(CreateLeaveRequestDto leaveRequest)
  {
    // make sure the requesting employee exist
    var employeeRepo = new EmployeeRepository(_context);
    bool employeeExists = await employeeRepo.EmployeeExists(leaveRequest.RequestingEmployeeId);

    if (!employeeExists)
    {
      throw new Exception("The employee associated with this leave request does not exist");
    }

    var newLeaveRequest = new LeaveRequest
    {
      RequestingEmployeeId = leaveRequest.RequestingEmployeeId,
      StartDate = leaveRequest.StartDate,
      EndDate = leaveRequest.EndDate,
      Status = LeaveRequestStatus.Pending,
      DateRequested = DateTime.Now
    };

    EntityEntry<LeaveRequest> addedLeaveRq = await _context.LeaveRequests.AddAsync(newLeaveRequest);
    await _context.SaveChangesAsync();
    return addedLeaveRq.Entity;
  }

  public async Task<LeaveRequest> UpdateLeaveRequest(AdminUpdateLeaveRequestDto dto)
  {
    // make sure the requesting employee exist
    var employeeRepo = new EmployeeRepository(_context);
    bool employeeExists = await employeeRepo.EmployeeExists(dto.RequestingEmployeeId);

    if (!employeeExists)
    {
      throw new Exception("The employee associated with this leave request does not exist");
    }

    var leaveRequest = await _context.LeaveRequests
      .FirstOrDefaultAsync(x => x.Id == dto.Id);

    if (leaveRequest == null) throw new Exception("Leave request not found");

    leaveRequest.RequestingEmployeeId = dto.RequestingEmployeeId;
    leaveRequest.StartDate = dto.StartDate;
    leaveRequest.EndDate = dto.EndDate;
    leaveRequest.Status = dto.Status;
    leaveRequest.DateUpdated = DateTime.Now;

    EntityEntry<LeaveRequest> updated = _context.LeaveRequests.Update(leaveRequest);
    await _context.SaveChangesAsync();
    return updated.Entity;
  }

  public async Task<LeaveRequest?> CancelLeaveRequest(int id)
  {
    return await UpdateLeaveRequestStatus(id, LeaveRequestStatus.Cancelled);
  }

  private async Task<LeaveRequest?> UpdateLeaveRequestStatus(int id, string status)
  {
    var leaveRequest = await _context.LeaveRequests
      .Include(static lvr => lvr.RequestingEmployee)
      .FirstOrDefaultAsync(x => x.Id == id);

    if (leaveRequest is null) return null;
    leaveRequest.Status = status;
    _context.LeaveRequests.Update(leaveRequest);
    await _context.SaveChangesAsync();

    return leaveRequest;
  }
}