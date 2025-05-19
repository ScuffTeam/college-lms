using System.Text.Json;
using college_lms.Data.DTOs.Auth;
using college_lms.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace college_lms.Services.Redis;

public class CacheService : IRefreshTokenStore, IJwtBlacklistStore
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<UserRefreshToken?> GetRefreshTokenAsync(string tokenKey)
    {
        var data = await _cache.GetAsync($"refresh:{tokenKey}");
        return data == null ? null : JsonSerializer.Deserialize<UserRefreshToken>(data);
    }

    public async Task SetRefreshTokenAsync(UserRefreshToken refreshToken)
    {
        var serialized = JsonSerializer.SerializeToUtf8Bytes(refreshToken);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = refreshToken.Expires - DateTime.UtcNow,
        };
        await _cache.SetAsync($"refresh:{refreshToken.Token}", serialized, options);
    }

    public async Task RevokeRefreshTokenAsync(string tokenKey)
    {
        await _cache.RemoveAsync($"refresh:{tokenKey}");
    }

    public async Task<bool> IsTokenInBlacklistAsync(string jti)
    {
        var exists = await _cache.GetAsync($"blacklist:{jti}");
        return exists != null;
    }

    public async Task AddToBlacklistAsync(string jti, TimeSpan expiration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration,
        };
        await _cache.SetAsync($"blacklist:{jti}", "revoked"u8.ToArray(), options);
    }
}
