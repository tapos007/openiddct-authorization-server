using System;
using IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.StartupExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            // Replace with your server version and type.
            // Use 'MariaDbServerVersion' for MariaDB.
            // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
            // For common usages, see pull request #1233.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            AuthDatabaseConfiguration(services,configuration,env,serverVersion);
            return services;
        }

        private static void AuthDatabaseConfiguration(IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment env, MySqlServerVersion serverVersion)
        {
            services.AddDbContext<ApplicationDbContext>(
                dbContextOptions =>
                {
                    dbContextOptions
                        .UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion);

                    dbContextOptions.UseOpenIddict();

                    if (!env.IsProduction())
                    {
                        dbContextOptions.EnableSensitiveDataLogging() // <-- These two calls are optional but help
                            .EnableDetailedErrors(); // <-- with debugging (remove for production).
                    }
                });
        }
    }
}