namespace Vertical.Api.Features.Products;

public class ProductRepository : IProductRepository
{
    public Task ArchiveProduct(Product product)
    {
        //throw new NotImplementedException();
        return Task.CompletedTask;
    }

    public Task<Product> CreateProductAsync(Product product)
    {
        return Task.FromResult(new Product() { Id = 1, Description = product.Description, Status = product.Status });
    }

    public Task<Product?> GetProduct(int id)
    {
        Product? produto = new Product() { Id = 1, Description = "Description", Status = Product.ProductStatus.Open };
        return Task.FromResult(produto);
    }
}