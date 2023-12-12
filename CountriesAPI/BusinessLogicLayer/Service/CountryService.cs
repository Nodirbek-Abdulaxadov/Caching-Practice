using CountriesAPI.BusinessLogicLayer.DTOs;
using CountriesAPI.BusinessLogicLayer.DTOs.CityDtos;
using CountriesAPI.BusinessLogicLayer.DTOs.CountryDtos;
using CountriesAPI.BusinessLogicLayer.Interfaces;
using CountriesAPI.DataLayer;
using CountriesAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.BusinessLogicLayer.Service;

public class CountryService : ICountryInterface
{
    private readonly AppDbContext _dbContext;
    public CountryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(AddCountryDto dto)
    {
        var country = (Country)dto;
        await _dbContext.Countries.AddAsync(country);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var country = _dbContext.Countries.FirstOrDefault(i => i.Id == id);
        _dbContext.Countries.Remove(country??
            throw new ArgumentNullException(nameof(country)));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CountryDto>> GetAll(Language language)
    {
        var list = await _dbContext.Countries.ToListAsync();
        var dtos = list.Select(c => c.ToDto(language))
                       .ToList();
        return dtos;
    }

    public async Task<CountryDto> GetById(int id, Language language)
    {
        var country = await _dbContext.Countries
                        .FirstOrDefaultAsync(i => i.Id == id);

        if (country == null)
        {
            throw new ArgumentNullException(nameof(country));
        }

        return country.ToDto(language);
    }

    public async Task Update(UpdateCountryDto dto)
    {
        _dbContext.Countries.Update((Country)dto);
        await _dbContext.SaveChangesAsync();
    }
}