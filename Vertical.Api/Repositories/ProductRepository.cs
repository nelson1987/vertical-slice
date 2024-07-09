using Vertical.Api.Features.Products;

namespace Vertical.Api.Repositories;

public class ProductRepository : IRepositoryRead<Product>
{
    public Task<Product> CreateAsync(Product product)
    {
        return Task.FromResult(new Product() { Id = 1, Description = product.Description, Status = product.Status });
    }

    public Task<Product?> Get(int id)
    {
        Product? produto = new Product() { Id = 1, Description = "Description", Status = Product.ProductStatus.Open };
        return Task.FromResult(produto);
    }

    public Task Update(Product product)
    {
        return Task.CompletedTask;
    }
}

public class ProductRepositoryWrite : IRepositoryWrite<Product>
{
    public Task<Product> CreateAsync(Product product)
    {
        return Task.FromResult(new Product() { Id = 1, Description = "product.Description" });
    }

    public Task<Product?> Get(int id)
    {
        return Task.FromResult(new Product() { Id = 1, Description = "product.Description" });
    }

    public Task UpdateProduct(Product product)
    {
        return Task.CompletedTask;
    }
}

public class ProductCacheService : ICacheService<Product>
{
    public Task SetCacheAsync(Product entity)
    {
        return Task.CompletedTask;
    }
}

public class ProductNotificationService : INotificationService<CreateProduct.ProductCreatedEvent>
{
    public Task SendEmailAsync(CreateProduct.ProductCreatedEvent entity)
    {
        return Task.CompletedTask;
    }

    public Task SendNotificationAsync(CreateProduct.ProductCreatedEvent entity)
    {
        return Task.CompletedTask;
    }
}