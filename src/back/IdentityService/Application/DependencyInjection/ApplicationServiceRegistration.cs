using Application.Common.Mediator;
using FluentValidation;
using IdentityService.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceRegistration).Assembly;

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IMediator, Mediator>();
        
        //Varre o assembly, encontra todas as classes que implementam IRequestHandler<,> e registra automaticamente:
        var handlerType = typeof(IRequestHandler<,>);

        var handlers = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType)
            ).ToList();
    
        foreach(var handler in handlers)
        {
            var servicesType = handler.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType);

            services.AddScoped(servicesType, handler);
        }

        return services;
    }
}
