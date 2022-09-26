using GConge.Models.DTOs.Auth;

namespace GConge.Web.Client.Services.Contracts;

public interface IUserLocalStorageService
{
  Task<UserDto?> GetUserFromLocalStorage();
  Task SaveUser(UserDto user);
  Task<UserDto?> RemoveCurrentUser();
  Task<UserDto?> GetUserFromServer();
  Task<string?> GetTokenAsync();

  Task<bool> CheckIfUserIsLoggedIn();
  Task<bool> CheckIfUserIsAdmin();
  Task<bool> AssertUserIsLoggedInOrRedirectToLogin(string redirectUrl);
}
