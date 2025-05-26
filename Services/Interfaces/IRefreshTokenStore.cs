using college_lms.Data.DTOs.Auth;

namespace college_lms.Services.Interfaces;

public interface IRefreshTokenStore
{
    Task<UserRefreshToken?> GetRefreshTokenAsync(string tokenKey);
    Task SetRefreshTokenAsync(UserRefreshToken refreshToken);
    Task RevokeRefreshTokenAsync(string tokenKey);
}
