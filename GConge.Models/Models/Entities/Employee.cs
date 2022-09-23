using System.ComponentModel.DataAnnotations.Schema;
using GConge.Models.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace GConge.Models.Models.Entities;

[Index(nameof(UserId), IsUnique = true)]
public sealed class Employee : BaseDomainEntity
{
  public string Service { get; set; }
  public int UserId { get; set; }

  [ForeignKey(nameof(UserId))]
  public User User { get; set; }
}
