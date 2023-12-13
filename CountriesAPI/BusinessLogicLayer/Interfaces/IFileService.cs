namespace CountriesAPI.BusinessLogicLayer.Interfaces;

public interface IFileService
{
    Task<string> SaveAsByteArray(IFormFile file);
    Task<byte[]> GetAsync(string url);
    string SaveFile(IFormFile file);
    void DeleteFile(string url);
}