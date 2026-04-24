using System.Security.Claims;

namespace IdentityService.Api.Extensions.DependencyInjection;

public static class AuthorizationExtenion
{
    public static IServiceCollection AddAuthorizationV1(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Verifica se é Admin ou não!
            options.AddPolicy("Admin", policy =>
                policy.RequireRole("Admin"));

            
            options.AddPolicy("User", policy =>
            //Faz validações para o usuario pode fazer as requisições!
            policy.RequireAssertion(ctx =>
            {
                var httpContext = ctx.Resource as HttpContext;
                var routeUserId = httpContext?.GetRouteValue("userId")?.ToString();
                var tokenUserId = ctx.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return routeUserId == tokenUserId;
            }));
            //Permite usuário e admin na mesma rota!
            options.AddPolicy("UserAndAdmin", policy =>
                policy.RequireAssertion(ctx =>
                {
                    if (ctx.User.IsInRole("Admin")) return true;

                    var httpContext = ctx.Resource as HttpContext;
                    var routeUserId = httpContext?.GetRouteValue("userId")?.ToString();
                    var tokenUserId = ctx.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return routeUserId == tokenUserId;
                }));
        });

        return services;
    }
}
