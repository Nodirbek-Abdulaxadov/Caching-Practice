using CountriesAPI.BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CountriesAPI.BusinessLogicLayer.Service;

public class RedisService : IRedisService
{
    private readonly IDistributedCache _cache;

    public RedisService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<string?> GetAsync(string key)
        => await _cache.GetStringAsync(key);

    public async Task<byte[]?> GetByteArrayAsync(string key)
        => await _cache.GetAsync(key);

    public async Task RemoveAsync(string key)
        => await _cache.RemoveAsync(key);

    public async Task SetAsync(string key, string value)
        => await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
            SlidingExpiration = TimeSpan.FromMinutes(1)
        });

    public async Task SetAsync(string key, byte[] value)
    {
        await _cache.SetAsync(key, value, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365)
        });
    }
}