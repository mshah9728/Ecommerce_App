namespace EcomApp.Configuration.Json
{
    public interface Ijson
    {
        string JsonSerializer<T>(T data);
        dynamic JsonDesializer(dynamic data);
    }
}
