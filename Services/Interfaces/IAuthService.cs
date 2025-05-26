using college_lms.Data.DTOs.Auth;

namespace college_lms.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponse> LoginAsync(string email, string password);
    Task<TokenResponse> RefreshTokenAsync(string refreshToken);
}
