namespace college_lms.Services.Interfaces;

public interface IJwtBlacklistStore
{
    Task<bool> IsTokenInBlacklistAsync(string jti);
    Task AddToBlacklistAsync(string jti, TimeSpan expiration);
}
