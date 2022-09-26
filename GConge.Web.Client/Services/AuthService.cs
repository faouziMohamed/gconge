using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GConge.Models.DTOs.Auth;
using GConge.Web.Client.Services.Contracts;

namespace GConge.Web.Client.Services;

public sealed class AuthService : IAuthService
{
  private readonly HttpClient _httpClient;


  public AuthService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }
  public async Task<UserDto?> Login(UserLoginRequestDto loginRequestDto)
  {
    var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequestDto);

    if (response.IsSuccessStatusCode)
    {
      return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    string message = await response.Content.ReadAsStringAsync();
    throw new Exception(message);
  }
  public async Task<UserDto?> Register(UserRegisterRequestDto registerRequestDto)
  {
    var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequestDto);

    if (response.IsSuccessStatusCode)
    {
      return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    string message = await response.Content.ReadAsStringAsync();
    throw new Exception(message);
  }
  public async Task<UserDto?> GetUserByToken(string bearerToken)
  {
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
    // make sure we have a success status code
    var response = await _httpClient.GetAsync("api/employee/current");
    // get the value if the status code is 200
    if (!response.IsSuccessStatusCode) return null;

    if (response.StatusCode == HttpStatusCode.OK)
    {
      return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    return null;
  }
}
