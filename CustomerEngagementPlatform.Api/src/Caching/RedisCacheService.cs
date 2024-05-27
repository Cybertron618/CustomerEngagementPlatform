using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerEngagementPlatform.Api.src.Caching
{
    public class RedisCacheService(IDistributedCache cache) : IRedisCacheService
    {
        private readonly IDistributedCache _cache = cache;

        public async Task SetCacheAsync<T>(string key, T value, DistributedCacheEntryOptions options)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, jsonData, options);
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            if (jsonData == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}

