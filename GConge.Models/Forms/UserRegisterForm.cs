using System.ComponentModel.DataAnnotations;
using GConge.Models.DTOs.Auth;

namespace GConge.Models.Forms;

public sealed record UserRegisterForm : UserRegisterRequestDto
{
  [Required] [Compare(nameof(Password), ErrorMessage = "Les mots de passe ne correspondent pas")]
  public string ConfirmPassword { get; set; } = default!;
}
