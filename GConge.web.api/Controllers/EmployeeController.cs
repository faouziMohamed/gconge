using System.Security.Claims;
using GConge.Models.DTOs.Employees;
using GConge.Models.Models;
using GConge.Models.Models.Entities;
using GConge.web.api.Extensions;
using GConge.web.api.Repositories.Contracts;
using GConge.web.api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GConge.web.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
  private readonly IEmployeeRepository _employeeRepository;
  private readonly IJwtAuthenticationService _jwtAuthenticationService;

  public EmployeeController(IEmployeeRepository employeeRepository, IJwtAuthenticationService jwtAuthenticationService)
  {
    _employeeRepository = employeeRepository;
    _jwtAuthenticationService = jwtAuthenticationService;
  }

  [Authorize]
  [HttpGet("Current")]
  public async Task<ActionResult> GetCurrentEmployee()
  {
    try
    {
      if (HttpContext.User.Identity is not ClaimsIdentity identity)
      {
        return Unauthorized();
      }

      var employee = _jwtAuthenticationService.GetEmployeeFromClaimsIdentity(identity);
      return Ok(await Task.FromResult(employee.ConvertToDto()));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize(Roles = UserRole.Admin)]
  [HttpGet]
  public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
  {
    try
    {
      List<Employee> employees = await _employeeRepository.GetEmployees();
      return Ok(employees.ConvertToDto());
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize(Roles = UserRole.Admin)]
  [HttpGet("{id:int}")]
  public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
  {
    try
    {
      var employee = await _employeeRepository.GetEmployeeById(id);
      if (employee is null) return NotFound("No employee with this id exists on the system");
      return Ok(employee.ConvertToDto());
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize(Roles = UserRole.Admin)]
  [HttpGet("{email}")]
  public async Task<ActionResult<EmployeeDto>> GetEmployeeByEmail(string email)
  {
    try
    {
      var employee = await _employeeRepository.GetEmployeeByEmail(email);
      if (employee is null) return NotFound($"No employee with this email {email} exists on the system");
      return Ok(employee.ConvertToDto());
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize(Roles = UserRole.Admin)]
  [HttpPost("search/{search}")]
  public async Task<ActionResult<List<EmployeeDto>>> SearchEmployees(string search)
  {
    try
    {
      List<Employee> employees = await _employeeRepository.SearchEmployees(search);
      return Ok(employees.ConvertToDto());
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
