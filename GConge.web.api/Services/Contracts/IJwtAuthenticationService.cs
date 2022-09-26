using System.Security.Claims;
using GConge.Models.Models.Entities;
using GConge.web.api.Models;

namespace GConge.web.api.Services.Contracts;

public interface IJwtAuthenticationService
{
  static public readonly int DefaultExpireMinutes = 120;

  string GenerateToken(Employee user, int? expireMinutes = null);
  bool ValidateToken(string token, out EmployeeClaim user);

  bool InvalidateToken(string token);
  Claim[] GetClaims(Employee employee);
  EmployeeClaim GetEmployeeFromPrincipal(ClaimsPrincipal principal);
  public EmployeeClaim GetEmployeeFromClaimsIdentity(ClaimsIdentity identity);
}
