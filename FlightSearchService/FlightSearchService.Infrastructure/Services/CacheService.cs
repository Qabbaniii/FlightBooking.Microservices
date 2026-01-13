
using FlightSearchService.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class CacheService(IDistributedCache Cache) : ICacheService
{

    public async Task<T?> GetAsync<T>(string cacheKey)
    {
        var cachevalue = await Cache.GetStringAsync(cacheKey);
        return cachevalue == null ? default : JsonSerializer.Deserialize<T>(cachevalue);
    }

    public async Task SetAsync<T>(string cacheKey, T cachevalue, TimeSpan TimeToLive)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeToLive
        };
        var value = JsonSerializer.Serialize(cachevalue);

        await Cache.SetStringAsync(cacheKey, value, options);
    }
}
