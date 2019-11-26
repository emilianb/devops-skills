using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;

namespace DevOps2017.EfForDotNetCore.Api
{
    public class DevOps2017DbContextFactory : IDbContextFactory<DevOps2017DbContext>
    {
        public DevOps2017DbContext Create()
        {
            var environmentName =
                        Environment.GetEnvironmentVariable(
                            "Hosting:Environment");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        public DevOps2017DbContext Create(DbContextFactoryOptions options)
        {
            return Create(
                options.ContentRootPath,
                options.EnvironmentName);
        }

        private DevOps2017DbContext Create(string basePath, string environmentName)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString("(default)");

            if (String.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException(
                    "Could not find a connection string named '(default)'.");
            }
            else
            {
                return Create(connstr);
            }
        }

        private DevOps2017DbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                    $"{nameof(connectionString)} is null or empty.",
                    nameof(connectionString));

            var optionsBuilder =
                new DbContextOptionsBuilder<DevOps2017DbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new DevOps2017DbContext(optionsBuilder.Options);
        }
    }
}




