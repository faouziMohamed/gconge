using Blazored.LocalStorage;
using GConge.Models.DTOs.Employees;
using GConge.Web.Client.Services.Contracts;

namespace GConge.Web.Client.Services;

/// <inheritdoc />
public sealed class EmployeeLocalStorageService : IEmployeeLocalStorageService
{
  private const string Key = "Employees";
  private readonly IEmployeeService _employeeService;
  private readonly ILocalStorageService _localStorageService;
  public EmployeeLocalStorageService(IEmployeeService employeeService, ILocalStorageService localStorageService)
  {
    _employeeService = employeeService;
    _localStorageService = localStorageService;
  }


  public async Task<List<EmployeeDto>> GetEmployeesFromLocalStorage()
  {
    var employees = await _localStorageService.GetItemAsync<List<EmployeeDto>>(Key);

    if (employees is not null) return employees;
    employees = await _employeeService.GetEmployees();
    await SaveEmployees(employees);
    return employees;
  }


  public async Task SaveEmployees(List<EmployeeDto> employees)
  {
    await _localStorageService.SetItemAsync(Key, employees);
  }

  public async Task AddEmployee(EmployeeDto employee)
  {
    List<EmployeeDto> employees = await GetEmployeesFromLocalStorage();
    if (employees.Any(e => e.EmployeeId == employee.EmployeeId)) return;
    employees.Add(employee);
    await SaveEmployees(employees);
  }

  public async Task UpdateEmployee(EmployeeDto employee)
  {
    List<EmployeeDto> employees = await GetEmployeesFromLocalStorage();
    var employeeToUpdate = employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
    if (employeeToUpdate is null) return;
    employees.Remove(employeeToUpdate);
    employees.Add(employee);
    await SaveEmployees(employees);
  }

  public async Task DeleteEmployee(int employeeId)
  {
    List<EmployeeDto> employees = await GetEmployeesFromLocalStorage();
    var employeeToDelete = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    if (employeeToDelete is null) return;
    employees.Remove(employeeToDelete);
    await SaveEmployees(employees);
  }


  public async Task<List<EmployeeDto>> ClearEmployees()
  {
    List<EmployeeDto> employees = await GetEmployeesFromLocalStorage();
    await _localStorageService.RemoveItemAsync(Key);
    return employees;
  }

  public async Task<EmployeeDto?> GetEmployeeFromLocalStorage(int employeeId)
  {
    List<EmployeeDto> employees = await GetEmployeesFromLocalStorage();
    return employees.FirstOrDefault(e => e.EmployeeId == employeeId);
  }
}
