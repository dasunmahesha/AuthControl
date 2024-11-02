using AuthControl.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuthControl.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _authService.LoginAsync(request);
        if (string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("Login failed for user: {Username}", request.Username);
            return Unauthorized("Invalid credentials");
        }

        _logger.LogInformation("User logged in successfully: {Username}", request.Username);
        return Ok(new { Token = token });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isSuccess = await _authService.RegisterUserAsync(registrationDto);
        if (!isSuccess)
        {
            _logger.LogWarning("Registration failed - Username already exists: {Username}", registrationDto.Username);
            return Conflict("Username already exists.");
        }

        _logger.LogInformation("User registered successfully: {Username}", registrationDto.Username);
        return Ok("User registered successfully");
    }

    [HttpGet("error")]
    public IActionResult GetError()
    {
        throw new Exception("This is a test exception");
    }
}
