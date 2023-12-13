using CountriesAPI.BusinessLogicLayer;
using CountriesAPI.BusinessLogicLayer.DTOs.CountryDtos;
using CountriesAPI.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CountriesAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryInterface _countryInterface;
    public CountryController(ICountryInterface countryInterface)
    {
        _countryInterface = countryInterface;
    }

    [HttpGet("{lang}")]
    //[ResponseCache(Duration = 30)] - client (frontchi) tomonidan cache qilinadi
    [OutputCache(Duration = 30)] //server tomonidan cache qilinadi
    public async Task<IActionResult> Get(string lang)
    {
        Language language = Language.uz;

        try
        {
            language = (Language)Enum.Parse(typeof(Language), lang.ToLower());
        }
        catch (Exception)
        {
            return BadRequest("Language is not supported!");
        }

        var list = await _countryInterface.GetAll(language);
        return Ok(list);
    }

    [HttpGet("{id}/{lang}")]
    public async Task<IActionResult> Get(int id, string lang)
    {
        Language language = Language.uz;

        try
        {
            language = (Language)Enum.Parse(typeof(Language), lang.ToLower());
        }
        catch (Exception)
        {
            return BadRequest("Language is not supported!");
        }
        
        try
        {
            var country = await _countryInterface.GetById(id, language);
            return Ok(country);
        }
        catch(ArgumentNullException)
        {
            return NotFound();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);

        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddCountryDto dto)
    {
        await _countryInterface.Add(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateCountryDto dto)
    {
        await _countryInterface.Update(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _countryInterface.Delete(id);
        return Ok();
    }
}