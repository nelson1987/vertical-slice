using FluentValidation;
using Vertical.Api.Features.Extensions;
using Vertical.Api.Repositories;

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

    public record ProductCreatedEvent(Product product)
    {
        internal static ProductCreatedEvent FromEntity(Product product)
        {
            return new ProductCreatedEvent(product);
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
            IValidator<Request> validator,
            IRepositoryRead<Product> readRepository,
            IRepositoryWrite<Product> writeRepository,
            ICacheService<Product> cacheService,
            INotificationService<ProductCreatedEvent> notificationService,
            CancellationToken cancellation = default)
        {
            //Criar Cliente
            //Incluir na base de leitura
            //Enviar email de boas vindas
            //Enviar push Notification de boas vindas
            //Enviar o dado para a base de cache(Redis)
            //Incluir dados na base de escrita(SQL Server)
            var validation = await validator.ValidateAsync(request, cancellation);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            Product produto = request.ToEntity();
            Product result = await readRepository.CreateAsync(produto);

            var events = ProductCreatedEvent.FromEntity(result);
            await notificationService.SendEmailAsync(events);
            await notificationService.SendNotificationAsync(events);

            await cacheService.SetCacheAsync(result);
            await writeRepository.CreateAsync(produto);

            //ISendEndpoint endpoint = await bus.GetSendEndpoint(new Uri("queue:orders_delivered"));
            //await endpoint.Send(cliente);
            Response response = Response.FromEntity(result);
            return Results.Created($"/{response.Id}", response);
        }
    }
}