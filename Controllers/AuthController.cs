using college_lms.Services;
using college_lms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace college_lms.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IAuthService _authService;

    public AuthController(IRefreshTokenStore refreshTokenStore, IAuthService authService)
    {
        _refreshTokenStore = refreshTokenStore;
        _authService = authService;
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var response = await _authService.RefreshTokenAsync(refreshToken);
        return Ok(response);
    }
}
