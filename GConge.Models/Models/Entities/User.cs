using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
  [JsonIgnore] public byte[] Password { get; set; }
  [JsonIgnore] public byte[] PasswordSalt { get; set; }
  public string Role { get; set; } = UserRole.User;
}
