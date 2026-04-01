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
                !string.IsNullOrEmpty(o.Issuer) &&
                !string.IsNullOrEmpty(o.Audience) &&
                !string.IsNullOrEmpty(o.SigningKey),
                $"Configuração {Infrastructure.Options.JwtOptions.SectionName} está inválida!")
            .ValidateOnStart();

        // 2) Configura JwtBearerOptions usando DI (IOptions<JwtOptions>)
        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<Infrastructure.Options.JwtOptions>>((options, jwtOptions) =>
            {
                var jwt = jwtOptions.Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Valida o emissor
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    
                    // Valida para quem vai
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,

                    //Converte a chave para byte
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey)),

                    //Valida tempo de vida do token
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(3)
                };
            });

        // 3) Registra Authentication/Authorization
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();

        return services;
    }
}