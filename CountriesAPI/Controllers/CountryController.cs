using CountriesAPI.BusinessLogicLayer;
using CountriesAPI.BusinessLogicLayer.DTOs.CountryDtos;
using CountriesAPI.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("/{lang}")]
    public async Task<IActionResult> Get(string lang)
    {
        var language = (Language)Enum.Parse(typeof(Language), lang);
        var list = await _countryInterface.GetAll(language);
        return Ok(list);
    }

    [HttpGet("/{id}/{lang}")]
    public async Task<IActionResult> Get(int id, string lang)
    {
        var language = (Language)Enum.Parse(typeof(Language), lang);
        var country = await _countryInterface.GetById(id, language);
        return Ok(country);
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