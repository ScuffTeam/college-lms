namespace college_lms.Services.Interfaces;

public interface IRefreshTokenStore
{
    Task<UserRefreshToken?> GetRefreshTokenAsync(string tokenKey);
    Task SetRefreshTokenAsync(string tokenKey, UserRefreshToken dto, TimeSpan expiration);
    Task RevokeRefreshTokenAsync(string tokenKey);
}
