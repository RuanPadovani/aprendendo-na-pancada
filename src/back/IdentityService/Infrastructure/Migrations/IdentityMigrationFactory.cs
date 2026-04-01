using IdentityService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Infrastructure.Persistence;

public class IdentityMigrationFactory 
    : IDesignTimeDbContextFactory<IdentityMigration>
{
    public IdentityMigration CreateDbContext(string[] args)
    {
        var apiPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Api");

        var config = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found!");

        var options = new DbContextOptionsBuilder<IdentityMigration>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        
        return new IdentityMigration(options);
    }
}
