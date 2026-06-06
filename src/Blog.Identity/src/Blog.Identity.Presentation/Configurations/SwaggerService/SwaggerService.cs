using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Models;

namespace Blog.Identity.Presentation.Configurations.SwaggerService;

public static class SwaggerService
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(cfg =>
        {
            cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authentication",
                Scheme = "Bearer",
                Type = SecuritySchemeType.Http
            });

            cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[]
                    {
                        
                    }
                }
            });
        });
        return services;
    }
}
