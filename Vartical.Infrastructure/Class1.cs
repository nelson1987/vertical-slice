using Microsoft.Extensions.DependencyInjection;
using Vertical.Domain;

namespace Vartical.Infrastructure;

private static IServiceCollection AddRepositories(this IServiceCollection services)
{
    services.AddScoped<IProductRepository, ProductRepository>();
    return services;
}

public class ProductRepository : IProductRepository
{
    public Task ArchiveProduct()
    {
        throw new NotImplementedException();
    }

    public Task CreateProductAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetProduct()
    {
        throw new NotImplementedException();
    }
}