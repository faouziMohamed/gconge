using GConge.Models.DTOs.Common;
using GConge.Models.Models.Entities;

namespace GConge.Models.DTOs.LeaveRequest;

public class LeaveRequestListDto : BaseDto
{
  public Employee Employee { get; set; }
  public string RequestingEmployeeId { get; set; }
  public DateTime DateRequested { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public bool? Approved { get; set; }
}
