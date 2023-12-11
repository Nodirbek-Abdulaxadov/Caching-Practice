using CountriesAPI.DataLayer.Entities;

namespace CountriesAPI.BusinessLogicLayer.DTOs.CountryDtos;

public class AddCountryDto
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string CapitalUz { get; set; } = string.Empty;
    public string CapitalRu { get; set; } = string.Empty;
    public string CapitalEn { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;


    public static implicit operator Country(AddCountryDto dto)
        => new()
        {
            NameEn = dto.NameEn,
            NameRu = dto.NameRu,
            NameUz = dto.NameUz,
            CapitalEn = dto.CapitalEn,
            CapitalRu = dto.CapitalRu,
            CapitalUz = dto.CapitalUz,
            Code = dto.Code,
            FlagUrl = dto.FlagUrl,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false
        };
}