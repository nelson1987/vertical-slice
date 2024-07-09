using Vertical.Api.Features.Extensions;
using Vertical.Api.Features.Products;
using Vertical.Api.Repositories;

namespace Vertical.Api;

public static class Dependencies
{
    public static IServiceCollection AddVerticalSlice(this IServiceCollection services)
    {
        services.AddEndpoints();
        services.AddRepositories();
        services.AddCacheServices();
        services.AddNotificationServices();
        services.AddCreateProducts();
        return services;
    }

    public static IApplicationBuilder UseVerticalSlice(this WebApplication app)
    {
        app.MapEndpoints();
        app.UseLogging(app.Logger);
        return app;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryRead<Product>, ProductRepository>();
        services.AddScoped<IRepositoryWrite<Product>, ProductRepositoryWrite>();
        return services;
    }

    private static IServiceCollection AddCacheServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheService<Product>, ProductCacheService>();
        return services;
    }

    private static IServiceCollection AddNotificationServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService<Product>, ProductNotificationService>();
        return services;
    }

    private static IApplicationBuilder UseLogging(this IApplicationBuilder app, ILogger logger)
    {
        app.Use(next =>
        {
            return async context =>
            {
                logger.LogInformation("Incoming request");
                logger.LogInformation("Started - TesteLog: {nameof}", "/api/customers");
                logger.LogInformation("Context | {nameof}", context.TraceIdentifier);
                await next(context);
                logger.LogInformation("Ended - TesteLog: {nameof}", "/api/customers");
                logger.LogInformation("Outgoing response");
            };
        });
        return app;
    }
}