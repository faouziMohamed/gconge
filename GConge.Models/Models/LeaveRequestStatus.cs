namespace GConge.Models.Models;

public sealed record LeaveRequestStatus
{
  public const string Pending = "En attente";
  public const string Approved = "Approuvé";
  public const string Rejected = "Rejeté";
  public const string Cancelled = "Annulé";
}
