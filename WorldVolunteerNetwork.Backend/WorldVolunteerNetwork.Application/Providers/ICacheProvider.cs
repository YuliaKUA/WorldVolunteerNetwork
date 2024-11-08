﻿
namespace WorldVolunteerNetwork.Application.Providers
{
    public interface ICacheProvider
    {
        Task<T?> GetAsync<T>(string key, CancellationToken ct = default) where T : class;
        Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken ct = default) where T : class;
        Task RemoveAsync(string key, CancellationToken ct = default);
        Task RemoveByPrefixAsync(string prefixKey, CancellationToken ct = default);
        Task SetAsync<T>(string key, T value, CancellationToken ct = default);
    }
}