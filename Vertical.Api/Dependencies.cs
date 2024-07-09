using Vertical.Api.Features.Extensions;
using Vertical.Api.Features.Products;
using Vertical.Api.Repositories;

namespace Vertical.Api.Features;

public static class Dependencies
{
    public static IServiceCollection AddVerticalSlice(this IServiceCollection services)
    {
        services.AddEndpoints();
        services.AddRepositories();
        services.AddCreateProducts();
        return services;
    }

    public static IApplicationBuilder UseVerticalSlice(this WebApplication app)
    {
        app.MapEndpoints();
        return app;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
