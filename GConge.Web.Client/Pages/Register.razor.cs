using System.ComponentModel.DataAnnotations;
using GConge.Models.DTOs.Auth;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GConge.Web.Client.Pages;

public class RegisterBase : ComponentBase

{

  protected readonly UserRegisterForm userRegisterRequestDto = new();

  [Inject]
  protected NavigationManager NavigationManager { get; set; }

  [Inject]
  protected IAuthService AuthService { get; set; } = default!;

  protected async Task OnSubmit()
  {
    Console.WriteLine($"{userRegisterRequestDto}");
    var user = await AuthService.Register(userRegisterRequestDto);

    if (user is not null)
    {
      NavigationManager.NavigateTo("/login");
      Console.WriteLine($"Hello {user.FirstName} {user.LastName}, you are registered");
    }
  }
}

public sealed record UserRegisterForm : UserRegisterRequestDto
{
  [Required] [Compare(nameof(Password), ErrorMessage = "Les mots de passe ne correspondent pas")]
  public string ConfirmPassword { get; set; } = default!;
}
