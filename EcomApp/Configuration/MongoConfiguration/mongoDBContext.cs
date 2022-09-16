using EcomApp.Models.ActivtyLogModel;
using EcomApp.Models.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EcomApp.DBConfigurations.MongoConfiguration
{
    public class MongoDBContext
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;
        public MongoDBContext (IConfiguration configuration)
        {
            _configuration = configuration;
            mongoClient = new MongoClient(_configuration["MongoDBConnection:ConnectionString"]);
            _database = mongoClient.GetDatabase(_configuration["MongoDBConnection:DatabaseName"]);
        }
        public IMongoCollection<AuditLogsModel> auditlogs => _database.GetCollection<AuditLogsModel>("AuditLogs");
        public IMongoCollection<ApplicationUser> users => _database.GetCollection<ApplicationUser>("Users");
    }
}
