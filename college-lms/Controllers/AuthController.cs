using college_lms.Data.DTOs.Auth;
using college_lms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace college_lms.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var response = await _authService.RefreshTokenAsync(refreshToken);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest.Email, loginRequest.Password);
        return Ok(response);
    }
}
