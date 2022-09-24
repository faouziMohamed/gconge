using GConge.Models.DTOs.Auth;

namespace GConge.Web.Client.Services.Contracts;

public interface IAuthService
{
  Task<UserResponseDto?> Login(UserLoginRequestDto loginRequestDto);
  Task<UserResponseDto?> Register(UserRegisterRequestDto registerRequestDto);
}
