using Blazored.LocalStorage;
using GConge.Models.DTOs.Auth;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GConge.Web.Client.Shared;

public class NavMenuBase : ComponentBase
{
  [Inject]
  protected IUserLocalStorageService UserLocalStorageService { get; set; } = default!;

  [Inject] protected IAuthService AuthService { get; set; } = default!;
  protected UserDto? ConnectedUser { get; set; }

  [Inject] protected NavigationManager Router { get; set; } = default!;

  [Inject]
  public ILocalStorageService LocalStorageService { get; set; }


  protected async override Task OnInitializedAsync()
  {
    ConnectedUser = await UserLocalStorageService.GetUserFromLocalStorage();

    if (ConnectedUser != null && !Router.Uri.Contains("login"))
    {
      var isUserStillConnected = await AuthService.GetUserByToken(ConnectedUser.AuthToken);
      if (isUserStillConnected == null) await Disconnect();
    }
  }

  protected async Task Disconnect()
  {
    string uri = Router.ToBaseRelativePath(Router.Uri);
    await LocalStorageService.ClearAsync();
    var redirectQuery = $"?redirectTo=/{uri}";
    Router.NavigateTo(uri: $"/login{(uri == "" ? "" : redirectQuery)}", true);
  }
}
