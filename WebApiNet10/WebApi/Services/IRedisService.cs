
namespace WebApi.Services;

public interface IRedisService
{
    Task DeleteValueAsync(string key);
    Task<T?> GetValueAsync<T>(string key);
    Task SetValueAsync<T>(string key, T data, TimeSpan? expiry = null);
}