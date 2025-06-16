using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using college_lms.Data.DTOs.Auth;
using college_lms.Data.Entities;
using college_lms.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace college_lms.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly string _secretKey;

    public AuthService(
        UserManager<User> userManager,
        IRefreshTokenStore refreshTokenStore,
        IOptions<AppOptions> options
    )
    {
        _userManager = userManager;
        _refreshTokenStore = refreshTokenStore;
        _secretKey = options.Value.SecretKey;
    }

    public async Task<TokenResponse> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var refreshToken = new UserRefreshToken
        {
            UserId = user.Id,
            Token = GenerateRefreshToken(),
            Expires = DateTime.UtcNow.AddDays(7),
        };

        await _refreshTokenStore.SetRefreshTokenAsync(refreshToken);

        return new TokenResponse
        {
            Token = await GenerateJwtToken(user),
            RefreshToken = refreshToken.Token,
            Expires = DateTime.UtcNow.AddHours(12),
        };
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var refreshTokenData = await _refreshTokenStore.GetRefreshTokenAsync(refreshToken);
        if (refreshTokenData == null || refreshTokenData.Expires < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid refresh token");

        var user = await _userManager.FindByIdAsync(refreshTokenData.UserId.ToString());
        if (user == null)
            throw new UnauthorizedAccessException("User not found");

        var newAccessToken = await GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        await _refreshTokenStore.SetRefreshTokenAsync(refreshTokenData);

        return new TokenResponse
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            Expires = DateTime.UtcNow.AddHours(12),
        };
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "self",
            audience: "users",
            claims: claims,
            expires: DateTime.Now.AddHours(12),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
