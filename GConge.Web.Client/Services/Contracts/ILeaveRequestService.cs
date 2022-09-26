using GConge.Models.DTOs.LeaveRequest;

namespace GConge.Web.Client.Services.Contracts;

public interface ILeaveRequestService
{
  Task<List<LeaveRequestDto>> GetLeaveRequests();
  Task<LeaveRequestDto?> GetLeaveRequestById(int leaveRequestId, int employeeId);
  Task<List<LeaveRequestDto>> GetLeaveRequestsByEmployeeId(int employeeId);
  Task<LeaveRequestDto> CreateLeaveRequest(CreateLeaveRequestDto leaveRequest);
  Task<LeaveRequestDto?> UpdateLeaveRequest(AdminUpdateLeaveRequestDto leaveRequest);
  Task<LeaveRequestDto?> ApproveLeaveRequest(int leaveRequestId);
  Task<LeaveRequestDto?> RejectLeaveRequest(int leaveRequestId);
  Task<LeaveRequestDto?> CancelLeaveRequest(int leaveRequestId);
  Task<LeaveRequestDto?> DeleteLeaveRequest(int leaveRequestId);
}
