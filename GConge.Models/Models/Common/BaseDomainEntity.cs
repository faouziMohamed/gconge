using System.ComponentModel.DataAnnotations;

namespace GConge.Models.Models.Common;

public abstract class BaseDomainEntity
{
  [Key]
  public int Id { get; set; }

  public DateTime DateCreated { get; set; } = DateTime.Now;
  public string CreatedBy { get; set; } = "System";
  public DateTime LastModifiedDate { get; set; } = DateTime.Now;
  public string LastModifiedBy { get; set; } = "System";
}
