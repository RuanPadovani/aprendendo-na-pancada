using Application.Interfaces;
using Application.Services;
using Application.UseCases.Auth;
using IdentityService.Application.Interfaces;
using IdentityService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;


namespace Application.ApplicationDependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<RefreshTokenUseCase>();
        return services;
    }
}
