namespace GConge.Models.Models;

public sealed record LeaveRequestStatus
{
  public const string Pending = "Pending";
  public const string Approved = "Approved";
  public const string Rejected = "Rejected";
  public const string Cancelled = "Cancelled";
}
