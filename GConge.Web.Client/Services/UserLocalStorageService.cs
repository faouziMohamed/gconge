using Blazored.LocalStorage;
using GConge.Models.DTOs.Auth;
using GConge.Models.Models;
using GConge.Web.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GConge.Web.Client.Services;

public sealed class UserLocalStorageService : IUserLocalStorageService
{
  private const string Key = "CurrentUser";
  private readonly IAuthService _authService;
  private readonly ILocalStorageService _localStorageService;
  private readonly NavigationManager _router;

  public UserLocalStorageService(ILocalStorageService localStorageService, IAuthService authService, NavigationManager router)
  {
    _localStorageService = localStorageService;
    _authService = authService;
    _router = router;
  }
  public async Task<UserDto?> GetUserFromLocalStorage()
  {
    // try to get the user if exist in the local storage
    return await _localStorageService.GetItemAsync<UserDto>(Key);
  }
  public async Task SaveUser(UserDto user)
  {
    await _localStorageService.SetItemAsync(Key, user);
  }
  public async Task<UserDto?> RemoveCurrentUser()
  {
    var user = await GetUserFromLocalStorage();
    await _localStorageService.RemoveItemAsync(Key);
    return user;
  }
  public async Task<UserDto?> GetUserFromServer()
  {
    // try to get update data of user from server by sending the current token
    var userFromStorage = await GetUserFromLocalStorage();
    if (userFromStorage is null) return null;
    string bearerToken = userFromStorage.AuthToken;
    var userFromServer = await _authService.GetUserByToken(bearerToken);
    if (userFromServer is null) return null;
    // update the user in the local storage
    await _localStorageService.SetItemAsync(Key, userFromServer);
    return userFromServer;
  }
  public async Task<string?> GetTokenAsync()
  {
    var user = await GetUserFromLocalStorage();
    return user?.AuthToken;
  }
  public async Task<bool> CheckIfUserIsLoggedIn()
  {
    var user = await GetUserFromLocalStorage();
    return user is not null;
  }
  public async Task<bool> CheckIfUserIsAdmin()
  {
    var user = await GetUserFromLocalStorage();
    return user?.Role == UserRole.Admin;
  }
  public async Task<bool> AssertUserIsLoggedInOrRedirectToLogin(string redirectUrl)
  {
    if (await CheckIfUserIsLoggedIn()) return true;
    _router.NavigateTo($"/login?redirectto={redirectUrl}");
    return false;
  }
}
