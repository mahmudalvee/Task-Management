using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrSetCacheAsync<T>(string cacheKey, Func<Task<T>> dataRetrievalFunction)
        {
            if (_memoryCache.TryGetValue(cacheKey, out T cacheData)) { }
            else
            {
                cacheData = await dataRetrievalFunction();      //get

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set(cacheKey, cacheData, cacheEntryOptions);
            }

            return cacheData;
        }
    }

}
