using GConge.Models.DTOs.Auth;
using GConge.Models.DTOs.Employees;
using GConge.Models.DTOs.LeaveRequest;
using GConge.Models.Models.Entities;
using GConge.web.api.Models;

namespace GConge.web.api.Extensions;

static public class DtoConverter
{
  static public UserDto ConvertToDto(this Employee employee, string token = "")
  {
    return new UserDto
    {
      FirstName = employee.User.Firstname,
      LastName = employee.User.Lastname,
      EmployeeId = employee.Id,
      Service = employee.Service,
      AuthToken = token,
      Role = employee.User.Role
    };
  }
  static public EmployeeDto ConvertToDto(this Employee employee)
  {
    return new EmployeeDto
    {
      EmployeeId = employee.Id,
      UserId = employee.UserId,
      FirstName = employee.User.Firstname,
      LastName = employee.User.Lastname,
      Service = employee.Service,
      Role = employee.User.Role
    };
  }

  static public List<EmployeeDto> ConvertToDto(this List<Employee> employees)
  {
    return employees.Select(static e => e.ConvertToDto()).ToList();
  }

  static public LeaveRequestDto ConvertToDto(this LeaveRequest leaveRequest)
  {
    return new LeaveRequestDto
    {
      LeaveRequestId = leaveRequest.Id,
      Employee = leaveRequest.RequestingEmployee.ConvertToDto(),
      StartDate = leaveRequest.StartDate,
      EndDate = leaveRequest.EndDate,
      Status = leaveRequest.Status,
      LeaveType = leaveRequest.LeaveType,
      DateRequested = leaveRequest.DateRequested
    };
  }

  static public List<LeaveRequestDto> ConvertToDto(this List<LeaveRequest> leaveRequests)
  {
    return leaveRequests.Select(static lr => lr.ConvertToDto()).ToList();
  }

  static public EmployeeDto ConvertToDto(this EmployeeClaim employeeClaim)
  {
    return new EmployeeDto
    {
      EmployeeId = employeeClaim.EmployeeId,
      FirstName = employeeClaim.Firstname,
      LastName = employeeClaim.Lastname,
      Role = employeeClaim.Role,
      Service = employeeClaim.Service,
      UserId = employeeClaim.UserId
    };
  }
}
