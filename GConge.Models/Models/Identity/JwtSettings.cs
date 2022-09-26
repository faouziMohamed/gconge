namespace GConge.Models.Models.Identity;

public sealed class JwtSettings
{
  public string Key { get; set; }
  public string Issuer { get; set; }
  public string Audience { get; set; }
  public double ExpirationInMinutes { get; set; }
}
