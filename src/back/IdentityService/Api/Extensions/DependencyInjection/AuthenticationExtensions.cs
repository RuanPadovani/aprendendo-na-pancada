using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions.DependencyInjection;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        // 1) Bind + validações para JwtOptions.
        services.AddOptions<Infrastructure.Options.JwtOptions>()
            .Bind(config.GetSection(Infrastructure.Options.JwtOptions.SectionName))
            .Validate(o =>
                !string.IsNullOrEmpty(o.ValidIssuer) &&
                !string.IsNullOrEmpty(o.ValidAudiences) &&
                !string.IsNullOrEmpty(o.SecretKey),
                $"Configuração {Infrastructure.Options.JwtOptions.SectionName} está inválida!")
            .ValidateOnStart();

        // 2) Configura JwtBearerOptions usando DI (IOptions<JwtOptions>)
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwt = config.GetSection(Infrastructure.Options.JwtOptions.SectionName)
                    .Get<Infrastructure.Options.JwtOptions>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.ValidIssuer,

                    ValidateAudience = true,
                    ValidAudience = jwt.ValidAudiences,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(3)
                };
            });

        services.AddAuthorization();

        return services;
    }
}