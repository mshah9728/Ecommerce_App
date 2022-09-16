using EcomApp.DBConfigurations.MongoConfiguration;
using EcomApp.Models.Roles;
using EcomApp.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using Abp.Extensions;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using EcomApp.Services.AuditLogs;
using EcomApp.Configuration.SQLConfiguration;
using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.JsonConfig;
using EcomApp.Services.Authorization;
using Microsoft.AspNetCore.Identity;
using EcomApp.Services.Product;
using EcomApp.Services.User;

namespace EcomApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string corsPolicy = "Cors";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //Adding Services to Projects
            var mongodb = Configuration.GetSection(nameof(MongoDBConnection)).Get<MongoDBConnection>();
            services.AddSingleton(serviceProvider => (IMongoDBConnection)serviceProvider.GetRequiredService<IOptions<MongoDBConnection>>().Value);
            services.AddScoped<PosAppDB>();
            //services.AddScoped<TransactionDBContext>();
            services.AddScoped<MongoDBContext>();
            services.AddTransient<IAuditLogsService, AuditLogsService>();
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongodb.ConnectionString, mongodb.DatabaseName).AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            services.AddTransient<ICacheMemory, Cachememory>();
            services.AddTransient<Ijson, _Json>();
            services.AddTransient<IAuthUserService, AuthUserService>();
            services.AddTransient<IProduct_LocationService, Product_LocationService>();
            services.AddTransient<IUserService, UserService>();
            //Adding Dependency Injection of Redis cache
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisConnection:Configuration"];
                options.InstanceName = Configuration["RedisConnection:instancename"];
            });

            //Adding SwaggerUi and Token Authentication in Project
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcomApp", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secert"])),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token", "True");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
          var cors =  services.AddCors(
                    options => options.AddPolicy(corsPolicy,
                        builder => builder.WithOrigins(
                                        // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                                        Configuration["App:Cors"]
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(o => o.RemovePostFix("/"))
                                        .ToArray()
                                    )
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials()
            )
        );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcomApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(corsPolicy);
            app.UseAuthorization();
            app.UseAuthentication();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
