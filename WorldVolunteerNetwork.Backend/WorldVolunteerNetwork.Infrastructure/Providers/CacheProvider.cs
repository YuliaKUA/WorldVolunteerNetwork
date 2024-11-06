using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;
using WorldVolunteerNetwork.Application.Providers;

namespace WorldVolunteerNetwork.Infrastructure.Providers
{
    public class CacheProvider : ICacheProvider
    {
        private readonly static ConcurrentDictionary<string, bool> _cacheKeys = new();
        private readonly IDistributedCache _cache;

        public CacheProvider(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
            where T : class
        {
            var cachedValue = await _cache.GetStringAsync(key, ct);

            if (cachedValue == null)
            {
                return null;
            }

            var value = JsonSerializer.Deserialize<T>(cachedValue);
            return value;
        }

        public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken ct = default)
            where T : class
        {
            var cachedValue = await GetAsync<T>(key, ct);

            if (cachedValue is not null)
            {
                return cachedValue;
            }

            cachedValue = await factory();
            await SetAsync<T>(key, cachedValue, ct);

            return cachedValue;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken ct = default)
        {
            var stringValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, stringValue);
            _cacheKeys.TryAdd(key, true);
        }

        public async Task RemoveAsync(string key, CancellationToken ct = default)
        {
            await _cache.RemoveAsync(key, ct);
            _cacheKeys.TryRemove(key, out _);
        }

        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken ct = default)
        {
            var tasks = _cacheKeys.Keys
                .Where(x => x.StartsWith(prefixKey.ToLower()))
                .Select(x => RemoveAsync(x, ct));

            await Task.WhenAll(tasks);
        }


    }
}
