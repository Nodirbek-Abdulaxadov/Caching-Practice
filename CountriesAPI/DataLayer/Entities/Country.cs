namespace CountriesAPI.DataLayer.Entities;

public class Country : BaseEntity
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string CapitalUz { get; set; } = string.Empty;
    public string CapitalRu { get; set; } = string.Empty;
    public string CapitalEn { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;

    public IEnumerable<City> Cities { get; set; } = new List<City>();
}