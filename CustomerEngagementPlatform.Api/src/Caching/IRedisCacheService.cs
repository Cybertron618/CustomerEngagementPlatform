using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace CustomerEngagementPlatform.Api.src.Caching
{
    public interface IRedisCacheService
    {
        Task SetCacheAsync<T>(string key, T value, DistributedCacheEntryOptions options);
        Task<T?> GetCacheAsync<T>(string key);
        Task RemoveCacheAsync(string key);
    }
}

