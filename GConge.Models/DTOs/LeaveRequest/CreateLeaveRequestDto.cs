namespace GConge.Models.DTOs.LeaveRequest;

public sealed record CreateLeaveRequestDto
{
  public int RequestingEmployeeId { get; init; }
  public DateTime StartDate { get; init; }
  public DateTime EndDate { get; init; }
}
