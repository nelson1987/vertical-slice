using FluentValidation;
using Vertical.Api.Features.Extensions;

namespace Vertical.Api.Features.Products;

/// <summary>
/// Create a new product in the system
/// </summary>
public static class CreateProduct
{
    public record Request(string Description)
    {
        internal Product ToEntity()
        {
            return new Product { Description = Description };
        }
    }

    public record Response(int Id, string Description, string status)
    {
        internal static Response FromEntity(Product product)
        {
            return new Response(product.Id, product.Description, product.Status.ToString());
        }
    }

    public static IServiceCollection AddCreateProducts(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Request>, Validator>();
        return services;
    }

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("products", Handler)
                .WithTags("Products")
                .WithName(nameof(CreateProduct))
                .WithOpenApi();
        }

        public static async Task<IResult> Handler(Request request,
            IProductRepository repository,
            IValidator<Request> validator,
            CancellationToken cancellation = default)
        {
            var validation = await validator.ValidateAsync(request, cancellation);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            Product produto = request.ToEntity();
            var result = await repository.CreateProductAsync(produto);
            Response response = Response.FromEntity(result);
            return Results.Created($"/{response.Id}", response);
        }
    }
}