using System.ComponentModel.DataAnnotations.Schema;
using GConge.Models.Models.Common;

namespace GConge.Models.Models.Entities;

public class LeaveRequest : BaseDomainEntity
{
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public DateTime DateRequested { get; set; } = DateTime.Now;
  public DateTime DateUpdated { get; set; } = DateTime.Now;
  public string Status { get; set; } = LeaveRequestStatus.Pending;
  public int RequestingEmployeeId { get; set; }

  [ForeignKey("RequestingEmployeeId")]
  public Employee RequestingEmployee { get; set; } = new();
}
