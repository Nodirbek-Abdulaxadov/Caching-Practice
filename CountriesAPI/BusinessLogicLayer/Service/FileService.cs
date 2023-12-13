using CountriesAPI.BusinessLogicLayer.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace CountriesAPI.BusinessLogicLayer.Service;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IConfiguration configuration;
    private readonly IRedisService redisService;

    public FileService(IWebHostEnvironment webHostEnvironment,
                       IConfiguration configuration,
                       IRedisService redisService)
    {
        this.webHostEnvironment = webHostEnvironment;
        this.configuration = configuration;
        this.redisService = redisService;
    }

    public string SaveFile(IFormFile file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads", fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return $"{configuration.GetValue<string>("DomainName") ?? ""}/uploads/{fileName}";
    }

    public void DeleteFile(string url)
    {
        if (url == null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        string fileName = url.Split('/')[^1];
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads", fileName);

        FileInfo file = new(filePath);
        if (file.Exists)
        {
            file.Delete();
        }
    }

    public async Task<string> SaveAsByteArray(IFormFile file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        string fileName = $"{Guid.NewGuid()}";

        byte[] fileBytes = ImageToByteArray(file);
        var str = string.Join(".", fileBytes);
        await redisService.SetAsync(fileName, str);
        return $"{configuration.GetValue<string>("DomainName") ?? ""}/uploads/{fileName}";
    }

    private byte[] ImageToByteArray(IFormFile file)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            using (Image image = Image.FromStream(memoryStream))
            {
                using (MemoryStream convertedMemoryStream = new MemoryStream())
                {
                    image.Save(convertedMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return convertedMemoryStream.ToArray();
                }
            }
        }
    }

    public async Task<byte[]> GetAsync(string url)
    {
        if (url == null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        var res = await redisService.GetAsync(url);
        var fileBytes = res.Split('.').Select(i => byte.Parse(i)).ToArray();
        return fileBytes;
    }
}