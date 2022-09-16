using EcomApp.Configuration.JsonConfig;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EcomApp.Configuration.CacheMemory
{

    public class Cachememory : ICacheMemory
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        private readonly Ijson _ijson;
        public Cachememory(IDistributedCache distributedCache, IConfiguration configuration, Ijson ijson)
        {
            _distributedCache = distributedCache;
            _configuration = configuration;
            _ijson = ijson;

        }
        public async void SavedCache(string keyname, string data)
        {
            if (keyname != null && data != null)
            {
                var tomorrowdate = DateTime.Now.AddDays(1);
                var totalseconds = tomorrowdate.Subtract(DateTime.Now).TotalSeconds;
                var distributedcacheentryOptions = new DistributedCacheEntryOptions();
                distributedcacheentryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(totalseconds);
                distributedcacheentryOptions.SlidingExpiration = null;
                await _distributedCache.SetStringAsync(keyname, data, distributedcacheentryOptions);
            }
            else
            {
                throw new Exception("ObjectName and Data is empty");
            }
            }
        public dynamic getDatafromKey(string key)
        {
            if (key == null)
                throw new Exception("Cache Key is not Provided");
            else
            {
                var data =  _distributedCache.GetString(key);
                var deserializedata = _ijson.JsonDesializer(data);
                if(data != null)
                {
                    return deserializedata;
                }
                else
                    throw new Exception("Something went wrong in Cache Services");
            }
        }
        public async Task<bool> RemoveDatafromCache(string key)
        {
            if (key == null)
            {
                throw new Exception("Key is invalid");
            }
            else
            {
                await _distributedCache.RemoveAsync(key);
                return true;
            }

        }
        public List<string> getAllcachekeyfromDB()
        {
            List<string> listkey = new List<string>();
            var connectionstring = _configuration["RedisConnection:Configuration"];
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_configuration["Redis:Connection"]))
            {
                
                var server = redis.GetServer(connectionstring);
                var keys = server.Keys();
                listkey.AddRange(keys.Select(keys => (string)keys).ToList());
                
            }
            return listkey;
        }
        public async Task RefreshCache(string key)
        {
            if (key == null)
                throw new Exception("Key cannot be null to refresh cache");
            else
            {
                await _distributedCache.RefreshAsync(key);
            }
        }
        public async Task RemoveAllCache(int databasename)
        {
            if (databasename > 0)
                throw new Exception("Database name cannot be null");
            else
            {
                var flushedKeys = new List<string>();
                var connectionstring = _configuration["RedisConnection:Configuration"];
                if (connectionstring != null)
                    throw new Exception("Make sure you using correct cache database connection string ");
                else
                {
                    var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(connectionstring);
                    using (connectionMultiplexer)
                    {
                        foreach (var endPoint in connectionMultiplexer.GetEndPoints())
                        {
                            var server = connectionMultiplexer.GetServer(endPoint);
                            var keys = server.Keys(databasename);
                            flushedKeys.AddRange(keys.Select(key => key.ToString()));
                            await server.FlushDatabaseAsync(databasename);
                        }
                    }
                }
            }
        }
    }
}
