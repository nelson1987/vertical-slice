namespace Vertical.Api.Features.Products;

public class Product
{
    public int Id { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Open;

    public enum ProductStatus
    {
        Open = 1,
        Closed = 2
    }
}