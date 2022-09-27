using System.Security.Claims;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models;
using GConge.Models.Models.Entities;
using GConge.web.api.Extensions;
using GConge.web.api.Repositories.Contracts;
using GConge.web.api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GConge.web.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveController : ControllerBase
{
  private readonly IEmployeeRepository _employeeRepository;
  private readonly IJwtAuthenticationService _jwtAuthenticationService;
  private readonly ILeaveRequestRepository _leaveRequestRepository;
  public LeaveController(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository,
    IJwtAuthenticationService jwtAuthenticationService)
  {
    _leaveRequestRepository = leaveRequestRepository;
    _employeeRepository = employeeRepository;
    _jwtAuthenticationService = jwtAuthenticationService;

  }
  [HttpGet]
  [Authorize(Roles = UserRole.Admin)]
  [Produces(typeof(List<LeaveRequestDto>))]
  public async Task<ActionResult> GetAll()
  {
    try
    {
      List<LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequests();
      return Ok(leaveRequests.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [HttpGet("{leaveRequestId:int}/{employeeId:int}")]
  [Authorize(Roles = $"{UserRole.User}, {UserRole.Admin}")]
  public async Task<ActionResult> GetById(int leaveRequestId, int employeeId)
  {
    try
    {
      if (!HasUserAccessToThisResource(employeeId)) return Unauthorized();
      var leaveRequest = await _leaveRequestRepository.GetLeaveRequestById(leaveRequestId);

      if (leaveRequest == null)
      {
        return NotFound($"No record found for this employeeId {leaveRequestId}");
      }

      return Ok(leaveRequest.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [HttpGet("employee/{employeeId:int}")]
  [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
  public async Task<ActionResult> GetByEmployeeId(int employeeId)
  {
    try
    {
      if (!HasUserAccessToThisResource(employeeId))
      {
        return Unauthorized("You are not authorized to access this resource");
      }

      List<LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestByEmployeeId(employeeId);
      return Ok(leaveRequests.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  private bool HasUserAccessToThisResource(int employeeId)
  {
    if (HttpContext.User.Identity is not ClaimsIdentity identity) return false;
    var employeeClaim = _jwtAuthenticationService.GetEmployeeFromClaimsIdentity(identity);
    if (employeeClaim.Role == UserRole.Admin) return true;
    return employeeClaim.EmployeeId == employeeId;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<LeaveRequestDto>> Create([FromBody] CreateLeaveRequestDto request)
  {
    try
    {
      if (!HasUserAccessToThisResource(request.RequestingEmployeeId))
      {
        return Unauthorized("You are not authorized to access this resource");
      }

      Console.WriteLine($"request: {request}");
      var leaveRequest = await _leaveRequestRepository.AddLeaveRequest(request);
      var employee = await _employeeRepository.GetEmployeeById(request.RequestingEmployeeId);
      leaveRequest.RequestingEmployee = employee!;
      return CreatedAtAction(
        actionName: nameof(GetById),
        routeValues: new
        {
          leaveRequestId = leaveRequest.Id,
          employeeId = leaveRequest.RequestingEmployeeId
        },
        value: leaveRequest.ConvertToDto()
      );
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [HttpPatch("{leaveRequestId:int}/approve")]
  [Authorize(Roles = UserRole.Admin)]
  public async Task<ActionResult> ApproveLeaveRequest(int leaveRequestId)
  {
    try
    {
      if (!await _leaveRequestRepository.LeaveRequestExists(leaveRequestId))
      {
        return NotFound($"No record found for this leaveRequestId {leaveRequestId}");
      }

      var updated = await _leaveRequestRepository.ApproveLeaveRequest(leaveRequestId);
      return Ok(updated!.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [HttpPatch("{leaveRequestId:int}/reject")]
  [Authorize(Roles = UserRole.Admin)]
  public async Task<ActionResult> RejectLeaveRequest(int leaveRequestId)
  {
    try
    {
      if (!await _leaveRequestRepository.LeaveRequestExists(leaveRequestId))
      {
        return NotFound($"No record found for this leaveRequestId {leaveRequestId}");
      }

      var updated = await _leaveRequestRepository.RejectLeaveRequest(leaveRequestId);
      return Ok(updated!.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [Authorize]
  [HttpPatch("{leaveRequestId:int}/cancel/by/{employeeId:int}")]
  public async Task<ActionResult> CancelLeaveRequest(int leaveRequestId, int employeeId)
  {
    try
    {
      if (!HasUserAccessToThisResource(employeeId))
      {
        Console.WriteLine($"Unauthorized {employeeId}");
        return Unauthorized("You are not authorized to access this resource");
      }

      if (!await _leaveRequestRepository.LeaveRequestExists(leaveRequestId))
      {
        return NotFound($"No record found for this leaveRequestId {leaveRequestId}");
      }

      var updated = await _leaveRequestRepository.CancelLeaveRequest(leaveRequestId);
      return Ok(updated!.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [HttpDelete("{leaveRequestId:int}/delete")]
  [Authorize(Roles = UserRole.Admin)]
  public async Task<ActionResult> Delete(int leaveRequestId)
  {
    try
    {
      if (!await _leaveRequestRepository.LeaveRequestExists(leaveRequestId))
      {
        return NotFound($"No record found for this leaveRequestId {leaveRequestId}");
      }

      var deleted = await _leaveRequestRepository.DeleteLeaveRequest(leaveRequestId);
      return Ok(deleted!.ConvertToDto());

    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }

  [HttpPut("update")]
  public async Task<ActionResult> Update([FromBody] UpdateLeaveRequestDto request)
  {
    try
    {
      if (!await _leaveRequestRepository.LeaveRequestExists(request.LeaveRequestId))
      {
        return NotFound($"No record found for this leaveRequestId {request.LeaveRequestId}");
      }

      var updated = await _leaveRequestRepository.UpdateLeaveRequest(request);
      return Ok(updated.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }
  }
}
