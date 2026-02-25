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
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
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
