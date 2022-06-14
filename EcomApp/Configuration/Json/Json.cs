using Newtonsoft.Json;

namespace EcomApp.Configuration.Json
{
    public class Json : Ijson
    {
        public string JsonSerializer<T>(T data)
        {
            var stringtojson = JsonConvert.SerializeObject(data);
            return stringtojson.ToString();
        }
        public dynamic JsonDesializer(dynamic data)
        {
            var jsontostring = JsonConvert.DeserializeObject<dynamic>(data);
            return jsontostring;
        }

        
    }
    
}
