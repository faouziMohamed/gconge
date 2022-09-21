using System.ComponentModel.DataAnnotations;
using GConge.Models.Models.Common;

namespace GConge.Models.Models;

public class User : BaseDomainEntity
{
  [EmailAddress]
  public string Email { get; set; }

  public string Firstname { get; set; }
  public string Lastname { get; set; }
  public string Password { get; set; }
  public string Role { get; set; } = "employee";

  [Phone]
  public string Phone { get; set; }
}
