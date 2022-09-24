using System.Net.Http.Json;
using GConge.Models.DTOs.Auth;

namespace GConge.Web.Client.Services.Contracts;

public sealed class AuthService : IAuthService
{
  private readonly HttpClient _httpClient;
  public AuthService(HttpClient httpClient)
  {
    _httpClient = httpClient;

  }
  public async Task<UserResponseDto?> Login(UserLoginRequestDto loginRequestDto)
  {
    var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequestDto);

    if (response.IsSuccessStatusCode)
    {
      return await response.Content.ReadFromJsonAsync<UserResponseDto>();
    }

    return null;
  }
  public async Task<UserResponseDto?> Register(UserRegisterRequestDto registerRequestDto)
  {
    var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequestDto);

    if (response.IsSuccessStatusCode)
    {
      return await response.Content.ReadFromJsonAsync<UserResponseDto>();
    }

    return null;
  }
}
