using CountriesAPI.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountriesAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null)
        {
            return BadRequest();
        }

        string fileName = await _fileService.SaveAsByteArray(file);

        return Ok(fileName);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]string fileUrl)
    {
        try
        {
            byte[] file = await _fileService.GetAsync(fileUrl);

            return File(file, "image/jpeg");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest();
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody]string fileUrl)
    {
        try
        {
            _fileService.DeleteFile(fileUrl);

            return Ok();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}