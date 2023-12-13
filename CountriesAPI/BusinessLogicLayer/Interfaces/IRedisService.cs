namespace CountriesAPI.BusinessLogicLayer.Interfaces;

public interface IRedisService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, string value);
    Task RemoveAsync(string key);

    Task SetAsync(string key, byte[] value);
    Task<byte[]?> GetByteArrayAsync(string key);
}