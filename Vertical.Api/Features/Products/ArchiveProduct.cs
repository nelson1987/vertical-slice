﻿using FluentValidation;
using Vertical.Api.Features.Extensions;
using Vertical.Api.Repositories;

namespace Vertical.Api.Features.Products;

/// <summary>
/// Archive a product
/// </summary>
public static class ArchiveProduct
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("products/{id}", Handler)
                .WithTags("Products")
                .WithName(nameof(ArchiveProduct))
                .WithOpenApi();
        }

        public static async Task<IResult> Handler(int id,
            IProductRepositoryRead repository,
            CancellationToken cancellation = default)
        {
            var product = await repository.GetProduct(id);
            if (product is null)
                return Results.NotFound();
            product.Status = Product.ProductStatus.Closed;
            await repository.ArchiveProduct(product);
            return Results.NoContent();
        }
    }
}