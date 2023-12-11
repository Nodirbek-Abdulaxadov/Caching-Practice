using CountriesAPI.BusinessLogicLayer.DTOs.CityDtos;
using CountriesAPI.DataLayer.Entities;

namespace CountriesAPI.BusinessLogicLayer.DTOs;

public static class Mapper
{
    public static CountryDto ToDto(this Country country,
                                        Language language)
        => new()
        {
            Id = country.Id,
            Code = country.Code,
            FlagUrl = country.FlagUrl,
            Name = language switch
            {
                Language.Uz => country.NameUz,
                Language.Ru => country.NameRu,
                Language.En => country.NameEn,
                _ => country.NameUz,
            },
            Capital = language switch
            {
                Language.Uz => country.CapitalUz,
                Language.Ru => country.CapitalRu,
                Language.En => country.CapitalEn,
                _ => country.CapitalUz,
            }
        };
}