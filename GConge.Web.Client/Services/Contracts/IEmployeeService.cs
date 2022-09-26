using GConge.Models.DTOs.Employees;

namespace GConge.Web.Client.Services.Contracts;

public interface IEmployeeService
{
  Task<List<EmployeeDto>> GetEmployees();
  Task<EmployeeDto> GetEmployeeById(int id);
  Task<EmployeeDto> GetEmployeeByEmail(string email);
  Task<EmployeeDto> UpdateEmployee(EmployeeDto employee);
  Task<List<EmployeeDto>> SearchEmployees(string searchTerm);
}
