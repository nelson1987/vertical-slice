namespace Vertical.Api.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);

    Task<Product?> GetProduct(int id);

    Task ArchiveProduct(Product product);
}