using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GConge.Models.Models.Entities;
using GConge.Models.Models.Identity;
using GConge.Models.Utils;
using GConge.web.api.Services.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace GConge.web.api.Services;

public sealed class JwtAuthenticationService : IJwtAuthenticationService
{
  public string GenerateToken(Employee employee, int expireMinutes)
  {
    bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    var jwtSettings = Utils.GetConfig<JwtSettings>(isDevelopment);
    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
    var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
      claims: GetClaims(employee),
      issuer: jwtSettings.Issuer,
      audience: jwtSettings.Audience,
      expires: DateTime.Now.AddMinutes(expireMinutes),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);

  }

  public bool InvalidateToken(string token)
  {
    bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    var jwtSettings = Utils.GetConfig<JwtSettings>(isDevelopment);
    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings.Issuer,
      ValidAudience = jwtSettings.Audience,
      IssuerSigningKey = secretKey
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    tokenHandler.ValidateToken(token, tokenValidationParameters, validatedToken: out var _);
    return true;
  }
  public Claim[] GetClaims(Employee employee)
  {
    return new[]
    {
      new Claim(ClaimTypes.Email, employee.User.Email),
      new Claim(ClaimTypes.Role, employee.User.Role),
      new Claim(ClaimTypes.GivenName, employee.User.Firstname),
      new Claim(ClaimTypes.Surname, employee.User.Lastname),
      new Claim(ClaimTypes.PrimarySid, value: employee.User.Id.ToString()),
      new Claim(ClaimTypes.Sid, value: employee.Id.ToString()),
      new Claim(ClaimTypes.UserData, employee.Service)
    };
  }

  public bool ValidateToken(string token, out Employee? employee)
  {
    bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    var jwtSettings = Utils.GetConfig<JwtSettings>(isDevelopment);
    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings.Issuer,
      ValidAudience = jwtSettings.Audience,
      IssuerSigningKey = secretKey
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, validatedToken: out var securityToken);

    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
        !jwtSecurityToken.Header.Alg
          .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
    {
      employee = null;
      return false;
    }

    employee = GetEmployeeFromPrincipal(principal);
    return true;
  }

  public Employee GetEmployeeFromClaimsIdentity(ClaimsIdentity identity)
  {
    List<Claim> claims = identity.Claims.ToList();
    return GetEmployeeFromClaims(claims);
  }
  public Employee GetEmployeeFromPrincipal(ClaimsPrincipal principal)
  {
    List<Claim> claims = principal.Claims.ToList();
    return GetEmployeeFromClaims(claims);
  }

  private static Employee GetEmployeeFromClaims(IReadOnlyCollection<Claim> claims)
  {
    string? email = claims.FirstOrDefault(static c => c.Type == ClaimTypes.Email)?.Value;
    string? role = claims.FirstOrDefault(static c => c.Type == ClaimTypes.Role)?.Value;
    string? firstname = claims.FirstOrDefault(static c => c.Type == ClaimTypes.GivenName)?.Value;
    string? lastname = claims.FirstOrDefault(static c => c.Type == ClaimTypes.Surname)?.Value;
    string? id = claims.FirstOrDefault(static c => c.Type == ClaimTypes.PrimarySid)?.Value;
    string? service = claims.FirstOrDefault(static c => c.Type == ClaimTypes.UserData)?.Value;
    string? employeeId = claims.FirstOrDefault(static c => c.Type == ClaimTypes.Sid)?.Value;

    return new Employee
    {
      Id = int.Parse(employeeId!),
      Service = service!,
      User = new User
      {
        Email = email!,
        Role = role!,
        Firstname = firstname!,
        Lastname = lastname!,
        Id = int.Parse(id!)
      }
    };
  }
}
