using System.ComponentModel.DataAnnotations;
using GConge.Models.DTOs.Employees;
using GConge.Models.Models;

namespace GConge.Models.DTOs.LeaveRequest;

public sealed record LeaveRequestDto
{
  [Required] public int LeaveRequestId { get; init; }
  [Required] public DateTime StartDate { get; set; }
  [Required] public DateTime EndDate { get; set; }
  [Required] public EmployeeDto Employee { get; init; }
  [Required] public DateTime DateRequested { get; init; }
  [Required] public string Status { get; set; } = LeaveRequestStatus.Pending;
  public string LeaveType { get; set; }
}
