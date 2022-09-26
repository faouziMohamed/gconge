using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using GConge.Models.DTOs.Employees;
using GConge.Web.Client.Services.Contracts;

namespace GConge.Web.Client.Services;

public sealed class EmployeeService : IEmployeeService
{
  private readonly HttpClient _httpClient;
  private readonly IUserLocalStorageService _localStorage;
  public EmployeeService(HttpClient httpClient, IUserLocalStorageService localStorage)
  {
    _httpClient = httpClient;
    _localStorage = localStorage;
  }
  public async Task<List<EmployeeDto>> GetEmployees()
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync("api/employee");
    if (!response.IsSuccessStatusCode || response.StatusCode != HttpStatusCode.OK) return new List<EmployeeDto>();
    var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
    return employees ?? new List<EmployeeDto>();
  }
  public async Task<EmployeeDto> GetEmployeeById(int id)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync($"api/employee/{id}");

    if (!response.IsSuccessStatusCode || response.StatusCode != HttpStatusCode.OK) return new EmployeeDto();
    var employee = await response.Content.ReadFromJsonAsync<EmployeeDto>();
    return employee ?? new EmployeeDto();

  }
  public async Task<EmployeeDto> GetEmployeeByEmail(string email)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync($"api/employee/email/{email}");
    if (!response.IsSuccessStatusCode || response.StatusCode != HttpStatusCode.OK) return new EmployeeDto();
    var employee = await response.Content.ReadFromJsonAsync<EmployeeDto>();
    return employee ?? new EmployeeDto();
  }
  public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employee)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.PutAsJsonAsync("api/employee", employee);
    if (!response.IsSuccessStatusCode || response.StatusCode != HttpStatusCode.OK) return new EmployeeDto();
    var updatedEmployee = await response.Content.ReadFromJsonAsync<EmployeeDto>();
    return updatedEmployee ?? new EmployeeDto();
  }
  public async Task<List<EmployeeDto>> SearchEmployees(string searchTerm)
  {
    await AddBearerTokenToHeader();
    var response = await _httpClient.GetAsync($"api/employee/search/{searchTerm}");
    if (!response.IsSuccessStatusCode || response.StatusCode != HttpStatusCode.OK) return new List<EmployeeDto>();
    var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
    return employees ?? new List<EmployeeDto>();
  }
  private async Task AddBearerTokenToHeader()
  {
    string? token = await _localStorage.GetTokenAsync();
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
  }
}
