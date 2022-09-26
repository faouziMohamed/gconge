namespace GConge.Models.DTOs.Employees;

public sealed record EmployeeDto
{
  public int EmployeeId { get; init; }
  public int UserId { get; init; }
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Service { get; init; }
  public string Role { get; init; }
}
