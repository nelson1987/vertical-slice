using FluentValidation;
using Vertical.Api.Features.Extensions;
using Vertical.Api.Repositories;

namespace Vertical.Api.Features.Products;

/// <summary>
/// Search through the product
/// </summary>
public static class GetProduct
{
    public record Response(int Id, string Description)
    {
        internal static Response FromEntity(Product product)
        {
            return new Response(product.Id, product.Description);
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("products/{id}", Handler)
                .WithTags("Products")
                .WithName(nameof(GetProduct))
                .WithOpenApi();
        }

        public static async Task<IResult> Handler(int id,
            IProductRepositoryRead repository,
            CancellationToken cancellation = default)
        {
            var product = await repository.GetProduct(id);
            if (product is null)
                return Results.NotFound();
            return Results.Ok(Response.FromEntity(product));
        }
    }
}