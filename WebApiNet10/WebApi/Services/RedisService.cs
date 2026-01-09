using StackExchange.Redis;
using System.Text.Json;

namespace WebApi.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase _database;

    public RedisService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    public async Task SetValueAsync<T>(string key, T data, TimeSpan? expiry = null)
    {
        var jsonData = JsonSerializer.Serialize(data);
        if (expiry.HasValue)
            await _database.StringSetAsync(key, jsonData, expiry.Value);
        else
            await _database.StringSetAsync(key, jsonData);
    }

    public async Task<T?> GetValueAsync<T>(string key)
    {
        var redisValue = await _database.StringGetAsync(key);

        if (!redisValue.HasValue)
            return default;

        var json = redisValue.ToString();
        return JsonSerializer.Deserialize<T>(json)!;
    }

    public async Task DeleteValueAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}
