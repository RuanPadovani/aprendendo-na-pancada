using IdentityService.Application.Interfaces;
using IdentityService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;


namespace Application.ApplicationDependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;   
    }
}
