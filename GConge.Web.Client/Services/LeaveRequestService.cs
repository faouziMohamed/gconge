using System.Net.Http.Headers;
using System.Net.Http.Json;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Web.Client.Services.Contracts;

namespace GConge.Web.Client.Services;

public class LeaveRequestService : ILeaveRequestService
{
  private readonly HttpClient _httpClient;
  private readonly IUserLocalStorageService _localStorage;
  public LeaveRequestService(HttpClient httpClient, IUserLocalStorageService localStorageService)
  {
    _httpClient = httpClient;
    _localStorage = localStorageService;
  }
  public async Task<List<LeaveRequestDto>> GetLeaveRequests()
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync("api/Leave");

    if (!response.IsSuccessStatusCode) return new List<LeaveRequestDto>();
    var leaves = await response.Content.ReadFromJsonAsync<List<LeaveRequestDto>>();
    return leaves ?? new List<LeaveRequestDto>();
  }
  public async Task<LeaveRequestDto?> GetLeaveRequestById(int leaveRequestId, int employeeId)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync($"api/Leave/{leaveRequestId}/{employeeId}");
    if (!response.IsSuccessStatusCode) return null;
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave;
  }
  public async Task<List<LeaveRequestDto>> GetLeaveRequestsByEmployeeId(int employeeId)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync($"api/Leave/employee/{employeeId}");
    if (!response.IsSuccessStatusCode) return new List<LeaveRequestDto>();
    var leaves = await response.Content.ReadFromJsonAsync<List<LeaveRequestDto>>();
    return leaves ?? new List<LeaveRequestDto>();
  }
  public async Task<LeaveRequestDto> CreateLeaveRequest(CreateLeaveRequestDto leaveRequest)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.PostAsJsonAsync("api/Leave", leaveRequest);
    if (!response.IsSuccessStatusCode) return new LeaveRequestDto();
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave!;
  }
  public async Task<LeaveRequestDto?> UpdateLeaveRequest(AdminUpdateLeaveRequestDto leaveRequest)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.PutAsJsonAsync("api/Leave", leaveRequest);
    if (!response.IsSuccessStatusCode) return null;
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave;
  }
  public async Task<LeaveRequestDto?> ApproveLeaveRequest(int leaveRequestId)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.PatchAsync(requestUri: $"api/Leave/approve/{leaveRequestId}", null);
    if (!response.IsSuccessStatusCode) return null;
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave;
  }
  public async Task<LeaveRequestDto?> RejectLeaveRequest(int leaveRequestId)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.PatchAsync(requestUri: $"api/Leave/reject/{leaveRequestId}", null);
    if (!response.IsSuccessStatusCode) return null;
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave;
  }
  public async Task<LeaveRequestDto?> CancelLeaveRequest(int leaveRequestId)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.PatchAsync(requestUri: $"api/Leave/cancel/{leaveRequestId}", null);
    if (!response.IsSuccessStatusCode) return null;
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave;
  }
  public async Task<LeaveRequestDto?> DeleteLeaveRequest(int leaveRequestId)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.DeleteAsync($"api/Leave/{leaveRequestId}");
    if (!response.IsSuccessStatusCode) return null;
    var leave = await response.Content.ReadFromJsonAsync<LeaveRequestDto>();
    return leave;
  }
  private async Task AddBearerTokenToHeader()
  {
    string? token = await _localStorage.GetTokenAsync();
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
  }
}
