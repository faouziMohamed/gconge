using System.Security.Claims;
using GConge.web.api.Extensions;
using GConge.web.api.Repositories.Contracts;
using GConge.web.api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GConge.web.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IEmployeeRepository _employeeRepository;
  private readonly IJwtAuthenticationService _jwtAuthenticationService;

  public UserController(IEmployeeRepository employeeRepository, IJwtAuthenticationService jwtAuthenticationService)
  {
    _employeeRepository = employeeRepository;
    _jwtAuthenticationService = jwtAuthenticationService;
  }

  [Authorize]
  [HttpGet("user")]
  public async Task<ActionResult<OkObjectResult>> GetUser()
  {
    try
    {
      if (HttpContext.User.Identity is not ClaimsIdentity identity)
      {
        return Unauthorized();
      }

      var employee = _jwtAuthenticationService.GetEmployeeFromClaimsIdentity(identity);
      return Ok(employee.ConvertToDto());
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
