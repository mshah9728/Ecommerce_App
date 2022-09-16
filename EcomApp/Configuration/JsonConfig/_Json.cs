using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Helpers;

namespace EcomApp.Configuration.JsonConfig
{
    public class _Json : Ijson
    {
        public string JsonSerializer<T>(T data)
        {
            var stringtojson = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return stringtojson.ToString();
        }
        public dynamic JsonDesializer(string data)
        {
            var jsontostring = JsonConvert.DeserializeObject<ExpandoObject>(data);
            return jsontostring;
        }

        public string Serializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public T Deserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

    }
    
}
