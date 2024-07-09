namespace Vertical.Api.Repositories;

public interface IRepositoryRead<T> where T : class
{
    Task<T> CreateAsync(T product);

    Task<T?> Get(int id);

    Task Update(T product);
}

public interface IRepositoryWrite<T> where T : class
{
    Task<T> CreateAsync(T product);

    Task<T?> Get(int id);

    Task UpdateProduct(T product);
}

public interface INotificationService<T> where T : class
{
    Task SendEmailAsync(T entity);

    Task SendNotificationAsync(T entity);
}

public interface ICacheService<T> where T : class
{
    Task SetCacheAsync(T entity);
}