namespace EcomApp.Configuration.JsonConfig
{
    public interface Ijson
    {
        string JsonSerializer<T>(T data);
        object JsonDesializer(string data);
        T Deserialize<T>(string jsonString);
        string Serializer<T>(T t);
    }
}
