using System.ComponentModel.DataAnnotations;
using GConge.Models.DTOs.Common;

namespace GConge.Models.DTOs.LeaveRequest;

public sealed class AdminUpdateLeaveRequestDto : BaseDto
{
  [Required] public int LeaveRequestId { get; set; }
  [Required] public string Status { get; set; }
  [Required] public DateTime StartDate { get; set; }
  [Required] public DateTime EndDate { get; set; }
  [Required] public DateTime? DateUpdated { get; set; } = DateTime.Now;
  [Required] public int RequestingEmployeeId { get; set; }
}
