namespace Api.Extensions.DependencyInjection;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsV1(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:5182","https://localhost:7179")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }
}
