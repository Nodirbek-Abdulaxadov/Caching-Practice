using CountriesAPI.DataLayer.Entities;

namespace CountriesAPI.BusinessLogicLayer.DTOs.CityDtos;

public class CountryDto : IdModel
{
    public string Name { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;
}