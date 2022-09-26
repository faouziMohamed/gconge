using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618
namespace GConge.Models.DTOs.Auth;

public sealed record UserLoginRequestDto
{
  [EmailAddress] [Required]
  public string Email { get; set; }

  [Required] [MinLength(6)]
  public string Password { get; set; }

  public bool RememberMe { get; init; } = false;
}
