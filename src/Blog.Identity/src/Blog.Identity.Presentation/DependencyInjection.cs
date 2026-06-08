using Blog.Identity.Application;
using Blog.Identity.Infrastructure;
using Blog.Identity.Persistence;
using Blog.Identity.Presentation.Configurations.SwaggerService;
using Blog.Identity.Presentation.Handlers;

namespace Blog.Identity.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationService(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerService();
        services.AddPersistenceService(config)
                .AddApplicationServices();
        services.AddProblemDetails();
        services.AddInfrastructureService(config);
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
