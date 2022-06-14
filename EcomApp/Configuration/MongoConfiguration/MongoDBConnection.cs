namespace EcomApp.DBConfigurations.MongoConfiguration
{
    public class MongoDBConnection : IMongoDBConnection
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }

    public interface IMongoDBConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
