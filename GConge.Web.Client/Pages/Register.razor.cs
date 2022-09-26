using Blazored.LocalStorage;
using GConge.Models.Forms;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GConge.Web.Client.Pages;

public class RegisterBase : ComponentBase
{

  protected bool IsLoading;
  protected UserRegisterForm UserRegister = new();

  [Inject]
  protected NavigationManager Router { get; set; }

  [Inject]
  protected IAuthService AuthService { get; set; } = default!;

  [Inject]
  private IUserLocalStorageService UserLocalStorage { get; set; } = default!;

  [Inject]
  private ILocalStorageService LocalStorage { get; set; } = default!;

  protected async override Task OnInitializedAsync()
  {
    IsLoading = true;
    bool isConnected = await UserLocalStorage.CheckIfUserIsLoggedIn();

    if (isConnected)
    {
      Router.NavigateTo("/LeaveRequests", true);
      return;
    }

    await LocalStorage.ClearAsync();
    IsLoading = false;
  }
}
