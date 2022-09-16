using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomApp.Configuration.CacheMemory
{
    public interface ICacheMemory
    {
        void SavedCache(string objectname, string data);
        dynamic getDatafromKey(string key);
        Task<bool> RemoveDatafromCache(string key);
        List<string> getAllcachekeyfromDB();
        Task RefreshCache(string key);
        Task RemoveAllCache(int databasename);
    }
}
