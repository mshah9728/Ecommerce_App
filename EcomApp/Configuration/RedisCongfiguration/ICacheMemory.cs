using System.Threading.Tasks;

namespace EcomApp.Configuration.CacheMemory
{
    public interface ICacheMemory
    {
        void SavedCache(string objectname, string data);
        Task<string> getDatafromKey(string key);
        void RemoveDatafromCache(string key);
    }
}
