using GConge.Models.DTOs.Employees;

namespace GConge.Web.Client.Services.Contracts;

public interface IEmployeeLocalStorageService
{
  /// <summary>
  ///   Get an employee from the local storage if it exists
  /// </summary>
  /// <param name="employeeId"></param>
  /// <returns></returns>
  Task<EmployeeDto?> GetEmployeeFromLocalStorage(int employeeId);
  /// <summary>
  ///   Get All Employees from Local Storage if not found then get from API <see cref="IEmployeeService" />
  /// </summary>
  /// <returns></returns>
  Task<List<EmployeeDto>> GetEmployeesFromLocalStorage();

  /// <summary>
  ///   Save the list of employees to local storage and return the list. This method will overwrite any existing list.
  /// </summary>
  /// <param name="employees"></param>
  Task SaveEmployees(List<EmployeeDto> employees);

  /// <summary>
  ///   Add one employee to the local storage
  /// </summary>
  /// <param name="employee"></param>
  Task AddEmployee(EmployeeDto employee);

  /// <summary>
  ///   Update an employee in the local storage if it exists, otherwise do nothing
  /// </summary>
  /// <param name="employee"></param>
  Task UpdateEmployee(EmployeeDto employee);

  /// <summary>
  ///   Delete an employee from the local storage if it exists
  /// </summary>
  /// <param name="employeeId"></param>
  Task DeleteEmployee(int employeeId);

  /// ///
  /// <summary>
  ///   Remove all employees from the local storage
  /// </summary>
  /// <returns></returns>
  Task<List<EmployeeDto>> ClearEmployees();
}
