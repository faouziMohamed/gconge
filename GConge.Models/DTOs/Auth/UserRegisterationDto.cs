using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

#pragma warning disable CS8618

namespace GConge.Models.DTOs.Auth;

public sealed record UserRegisterRequestDto
{
  [EmailAddress] public string Email { get; init; }
  [Required] [MinLength(6)] public string Password { get; init; }
  [Required] public string FirstName { get; init; }
  [Required] public string LastName { get; init; }
  [Phone] public string PhoneNumber { get; init; }
  [Required] public string Service { get; init; }
}
