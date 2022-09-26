using GConge.Models.DTOs.Auth;

namespace GConge.Web.Client.Services.Contracts;

public interface IAuthService
{
  Task<UserDto?> Login(UserLoginRequestDto loginRequestDto);
  Task<UserDto?> Register(UserRegisterRequestDto registerRequestDto);
  Task<UserDto?> GetUserByToken(string bearerToken);
}
