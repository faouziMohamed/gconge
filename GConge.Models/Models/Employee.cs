using GConge.Models.Models.Common;

namespace GConge.Models.Models;

public sealed class Employee : BaseDomainEntity
{
  public string Service { get; set; }
  public int UserId { get; set; }
  public User User { get; set; }
}
