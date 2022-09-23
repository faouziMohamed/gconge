using GConge.Models.DTOs.Common;

namespace GConge.Models.DTOs.LeaveRequest;

public class ChangeLeaveRequestApprovalDto : BaseDto
{
  public bool Approved { get; set; }
}
