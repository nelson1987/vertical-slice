namespace Vertical.Domain;

public interface IProductRepository
{
    Task CreateProductAsync();

    Task GetProduct();

    Task ArchiveProduct();
}