namespace GConge.web.api.Models;

public sealed class EmployeeClaim
{
  public int EmployeeId { get; set; }
  public int UserId { get; set; }
  public string Service { get; set; } = default!;
  public string Email { get; set; } = default!;
  public string Role { get; set; } = default!;
  public string Firstname { get; set; } = default!;
  public string Lastname { get; set; } = default!;
}
