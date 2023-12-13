using CountriesAPI.BusinessLogicLayer.DTOs;
using CountriesAPI.BusinessLogicLayer.DTOs.CityDtos;
using CountriesAPI.BusinessLogicLayer.DTOs.CountryDtos;
using CountriesAPI.BusinessLogicLayer.Interfaces;
using CountriesAPI.DataLayer;
using CountriesAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CountriesAPI.BusinessLogicLayer.Service;

public class CountryService : ICountryInterface
{
    private readonly AppDbContext _dbContext;
    private readonly IRedisService _redisService;
    private const string CACHE_KEY = "countries";

    public CountryService(AppDbContext dbContext,
                          IRedisService redisService)
    {
        _dbContext = dbContext;
        _redisService = redisService;
    }

    public async Task Add(AddCountryDto dto)
    {
        var country = (Country)dto;
        await _dbContext.Countries.AddAsync(country);
        await _dbContext.SaveChangesAsync();
        await _redisService.RemoveAsync(CACHE_KEY);
    }

    public async Task Delete(int id)
    {
        var country = _dbContext.Countries.FirstOrDefault(i => i.Id == id);
        _dbContext.Countries.Remove(country??
            throw new ArgumentNullException(nameof(country)));
        await _dbContext.SaveChangesAsync();
        await _redisService.RemoveAsync(CACHE_KEY);
    }

    public async Task<string> GetAll(Language language)
    {
        var data = await _redisService.GetAsync(CACHE_KEY);
        if (data != null)
        {
            var dtoList = JsonConvert.DeserializeObject<List<Country>>(data)
                                     .Select(c => c.ToDto(language))
                                     .ToList();
            var jsonData = JsonConvert.SerializeObject(dtoList, Formatting.Indented);
            return jsonData;
        }

        var list = await _dbContext.Countries.ToListAsync();
        await _redisService.SetAsync(CACHE_KEY, JsonConvert.SerializeObject(list));
        var dtos = list.Select(c => c.ToDto(language))
                       .ToList();

        var json = JsonConvert.SerializeObject(dtos, Formatting.Indented);
        return json;
    }

    public async Task<string> GetById(int id, Language language)
    {
        var data = await GetAll(language);
        var dto = JsonConvert.DeserializeObject<List<CountryDto>>(data)
                                .FirstOrDefault(c => c.Id == id);
        return JsonConvert.SerializeObject(dto, Formatting.Indented);
    }

    public async Task Update(UpdateCountryDto dto)
    {
        _dbContext.Countries.Update((Country)dto);
        await _dbContext.SaveChangesAsync();
        await _redisService.RemoveAsync(CACHE_KEY);
    }
}