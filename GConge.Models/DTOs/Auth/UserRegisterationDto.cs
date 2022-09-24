using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

#pragma warning disable CS8618

namespace GConge.Models.DTOs.Auth;

public record UserRegisterRequestDto
{
  [EmailAddress] public string Email { get; set; }
  [Required] [MinLength(6)] [PasswordPropertyText] public string Password { get; set; }
  [Required] public string FirstName { get; set; }
  [Required] public string LastName { get; set; }
  [Required] public string Service { get; set; }
}
