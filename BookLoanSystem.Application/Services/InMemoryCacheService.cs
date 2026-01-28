public class InMemoryCacheService : ICacheService
{
    private class CacheItem
    {
        public object Value { get; set; }
        public DateTimeOffset? Expiry { get; set; }
    }

    private readonly Dictionary<string, CacheItem> _cache = new();

    public T? Get<T>(string key)
    {
        lock (_cache)
        {
            if (_cache.TryGetValue(key, out var item))
            {
                if (item.Expiry.HasValue && item.Expiry.Value < DateTimeOffset.UtcNow)
                {
                    _cache.Remove(key);
                    return default;
                }

                if (item.Value is T value)
                    return value;
            }
        }
        return default;
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        var expiryTime = expiry.HasValue ? DateTimeOffset.UtcNow.Add(expiry.Value) : (DateTimeOffset?)null;
        var item = new CacheItem
        {
            Value = value!,
            Expiry = expiryTime
        };
        lock (_cache)
        {
            _cache[key] = item;
        }
    }

    public void Remove(string key)
    {
        lock (_cache)
        {
            _cache.Remove(key);
        }
    }
}