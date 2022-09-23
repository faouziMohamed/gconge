namespace GConge.Models.DTOs.Auth;
#pragma warning disable CS8618

public record UserResponseDto
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public int EmployeeId { get; init; }
  public string Service { get; init; }
  public string AuthToken { get; init; }
  public string Role { get; init; }
}
