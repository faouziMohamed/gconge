using System.ComponentModel.DataAnnotations.Schema;
using GConge.Models.Models.Common;

namespace GConge.Models.Models;

public class LeaveRequest : BaseDomainEntity
{
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public DateTime DateRequested { get; set; }
  public bool? Approved { get; set; }
  public bool Cancelled { get; set; }
  public string Status { get; set; }
  public int RequestingEmployeeId { get; set; }

  [ForeignKey("RequestingEmployeeId")]
  public Employee RequestingEmployee { get; set; }
}
