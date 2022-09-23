using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models.Entities;

namespace GConge.web.api.Repositories.Contracts;

public interface ILeaveRequestRepository
{
  Task<LeaveRequest?> GetLeaveRequestById(int id);
  Task<List<LeaveRequest>> GetLeaveRequests();
  Task<LeaveRequest> AddLeaveRequest(CreateLeaveRequestDto createLeaveRequestDto);
  Task<LeaveRequest> UpdateLeaveRequest(AdminUpdateLeaveRequestDto dto);
  Task<LeaveRequest> DeleteLeaveRequest(int id);
  Task<LeaveRequest?> ApproveLeaveRequest(int id);
  Task<LeaveRequest?> RejectLeaveRequest(int id);
  public Task<LeaveRequest?> CancelLeaveRequest(int id);

  Task<bool> LeaveRequestExists(int id);
}
