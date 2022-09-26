using GConge.Models.DTOs.Auth;
using GConge.Models.Models;
using GConge.Models.Models.Entities;
using GConge.Models.Utils;
using GConge.web.api.Extensions;
using GConge.web.api.Repositories.Contracts;
using GConge.web.api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GConge.web.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IEmployeeRepository _employeeRepository;
  private readonly IJwtAuthenticationService _jwtAuthenticationService;
  private readonly IUserRepository _userRepository;

  public AuthController(IUserRepository userRepository, IEmployeeRepository employeeRepository,
    IJwtAuthenticationService jwtAuthenticationService)
  {
    _userRepository = userRepository;
    _employeeRepository = employeeRepository;
    _jwtAuthenticationService = jwtAuthenticationService;
  }

  [AllowAnonymous]
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto request, [FromQuery] bool isAdmin = false)
  {
    try
    {
      // Check if the user already exists
      bool user = await _userRepository.UserExists(request.Email);

      if (user) return BadRequest($"The email {request.Email} is already in use.");
      byte[] passwordHash = Utils.HashPassword(request.Password, salt: out byte[] passwordSalt);
      var newUser = new User
      {
        Email = request.Email,
        Password = passwordHash,
        PasswordSalt = passwordSalt,
        Firstname = request.FirstName,
        Lastname = request.LastName,
        Role = isAdmin ? UserRole.Admin : UserRole.User
      };

      var createdUser = await _userRepository.CreateUser(newUser);
      var newEmployee = new Employee
      {
        UserId = createdUser.Id,
        Service = request.Service
      };

      var employee = await _employeeRepository.CreateEmployee(newEmployee);

      return StatusCode(StatusCodes.Status201Created, value: employee.ConvertToDto());
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status400BadRequest, e.Message);
    }
  }

  [AllowAnonymous]
  [HttpPost("login")]
  [ProducesResponseType(type: typeof(UserDto), StatusCodes.Status200OK)]
  public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
  {
    try
    {
      var user = await _userRepository.GetUserByEmail(request.Email);
      if (user == null) return BadRequest("Unable to find a user with this email.");

      bool passwordMatch = Utils.VerifyHashedPassword(request.Password, user.Password, user.PasswordSalt);
      if (!passwordMatch) return BadRequest("Incorrect password.");

      var employee = await _employeeRepository.GetEmployeeByUserId(user.Id);
      string token = _jwtAuthenticationService.GenerateToken(user: employee!);

      var userResponseDto = new UserDto
      {
        FirstName = user.Firstname,
        LastName = user.Lastname,
        EmployeeId = employee!.Id,
        Service = employee.Service,
        AuthToken = token,
        Role = user.Role
      };

      return Ok(userResponseDto);
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status400BadRequest, e.Message);
    }
  }
}
