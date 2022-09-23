using System.Security.Claims;
using GConge.Models.Models.Entities;

namespace GConge.web.api.Services.Contracts;

public interface IJwtAuthenticationService
{
  static public readonly int DefaultExpireMinutes = 120;

  string GenerateToken(Employee user, int expireMinutes = 120);
  bool ValidateToken(string token, out Employee? user);

  bool InvalidateToken(string token);
  Claim[] GetClaims(Employee employee);
  Employee GetEmployeeFromPrincipal(ClaimsPrincipal principal);
  public Employee GetEmployeeFromClaimsIdentity(ClaimsIdentity identity);
}
