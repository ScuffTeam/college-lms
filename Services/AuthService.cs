using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using college_lms.Data.DTOs.Auth;
using college_lms.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace college_lms.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly string _secretKey;

    public AuthService(UserManager<IdentityUser> userManager, IOptions<AppOptions> options)
    {
        _userManager = userManager;
        _secretKey = options.Value.SecretKey;
    }

    public async Task<TokenResponse> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid credentials");

        return await GenerateJwtToken(user);
    }

    private async Task<TokenResponse> GenerateJwtToken(IdentityUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(
                JwtRegisteredClaimNames.Email,
                user.Email
                    ?? throw new ArgumentNullException(nameof(user.Email), "User email is null")
            ),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Add roles as claims
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: "self",
            audience: "users",
            claims: claims,
            expires: DateTime.Now.AddHours(12),
            signingCredentials: credentials
        );

        return new TokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = token.ValidTo,
        };
    }
}
