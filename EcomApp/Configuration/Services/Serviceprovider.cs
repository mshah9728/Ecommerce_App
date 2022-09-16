using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.JsonConfig;
using EcomApp.Configuration.SQLConfiguration;
using EcomApp.DBConfigurations.MongoConfiguration;
using EcomApp.Models.Roles;
using EcomApp.Models.Users;
using EcomApp.Services.AuditLogs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;

namespace EcomApp.Configuration.Services
{
    public class Serviceprovider :IServicesprovider
    {
        public IConfiguration Configuration { get; }
        public void Services(IServiceCollection services)
        {
            
        }
    }
}
