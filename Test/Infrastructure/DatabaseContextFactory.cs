using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Test.Infrastructure {
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext> {
        public DatabaseContext CreateDbContext(string[] args) {
            var optionBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            var path = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            var connectionString = configuration.GetConnectionString("SQLConnection");

            Console.WriteLine($"connectionString: {connectionString}");

            optionBuilder.UseNpgsql(connectionString);
            return new DatabaseContext(optionBuilder.Options);
        }
    }
}