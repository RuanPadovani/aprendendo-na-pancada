using IdentityService.Application.Interfaces;
using Infrastructure.Repositories.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.InfrastructureDependencyInjection;

public static class DependencyInejction
{
    public static IServiceCollection AddInfrastructue(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
