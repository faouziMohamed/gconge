using GConge.Models.Models.Entities;

namespace GConge.web.api.Repositories.Contracts;

public interface IEmployeeRepository
{
  Task<Employee?> GetEmployeeById(int employeeId);
  Task<Employee?> GetEmployeeByUserId(int userId);
  Task<List<Employee>> GetEmployees();
  Task<Employee> CreateEmployee(Employee employee);
  Task<Employee> UpdateEmployee(Employee employee);
  Task<Employee?> DeleteEmployee(int id);
  Task<bool> EmployeeExists(int id);
  Task<List<Employee>> SearchEmployees(string search);
  Task<Employee?> GetEmployeeByEmail(string email);
}
