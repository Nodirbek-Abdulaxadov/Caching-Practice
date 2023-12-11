using CountriesAPI.BusinessLogicLayer.DTOs.CityDtos;
using CountriesAPI.BusinessLogicLayer.DTOs.CountryDtos;

namespace CountriesAPI.BusinessLogicLayer.Interfaces;

public interface ICountryInterface
{
    Task<List<CountryDto>> GetAll(Language language);
    Task<CountryDto> GetById(int id, Language language);
    Task Add(AddCountryDto dto);
    Task Update(UpdateCountryDto dto);
    Task Delete(int id);
}