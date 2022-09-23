using System.ComponentModel.DataAnnotations;
using GConge.Models.Models.Common;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedAutoPropertyAccessor.Global

#pragma warning disable CS8618

namespace GConge.Models.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
public sealed class User : BaseDomainEntity
{
  [EmailAddress]
  public string Email { get; set; }

  public string Firstname { get; set; }
  public string Lastname { get; set; }
  public byte[] Password { get; set; }
  public string Role { get; set; } = UserRole.User;

  [Phone]
  public string PhoneNumber { get; set; }

  public byte[] PasswordSalt { get; set; }
}
