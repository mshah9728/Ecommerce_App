using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace EcomApp.Configuration.CacheMemory
{

    public class Cachememory : ICacheMemory
    {
        private readonly IDistributedCache _distributedCache;
        public Cachememory(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async void SavedCache(string objectname, string data)
        {
            if (objectname != null && data != null)
            {
                var tomorrowdate = DateTime.Now.AddDays(1);
                var totalseconds = tomorrowdate.Subtract(DateTime.Now).TotalSeconds;
                var distributedcacheentryOptions = new DistributedCacheEntryOptions();
                distributedcacheentryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(totalseconds);
                distributedcacheentryOptions.SlidingExpiration = null;
                await _distributedCache.SetStringAsync(objectname, data, distributedcacheentryOptions);
            }
            else
            {
                throw new Exception("ObjectName and Data is empty from controller");
            }
        }
        public async Task<string> getDatafromKey(string key)
        {
            if (key == null)
                throw new Exception("Cache Key is not Provided");
            else
            {
                var data = await _distributedCache.GetStringAsync(key);
                return data;
            }
        }
        public async void RemoveDatafromCache(string key)
        {
            if (key == null)
            {
                throw new Exception("Key is empty");
            }
            else
            {
                await _distributedCache.RemoveAsync(key);
            }

        }
    }
}
