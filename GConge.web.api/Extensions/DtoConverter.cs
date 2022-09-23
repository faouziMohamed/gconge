using GConge.Models.DTOs.Auth;
using GConge.Models.Models.Entities;

namespace GConge.web.api.Extensions;

static public class DtoConverter
{
  static public UserResponseDto ConvertToDto(this Employee employee, string token = "")
  {
    return new UserResponseDto
    {
      FirstName = employee.User.Firstname,
      LastName = employee.User.Lastname,
      EmployeeId = employee!.Id,
      Service = employee.Service,
      AuthToken = token,
      Role = employee.User.Role
    };
  }
}
