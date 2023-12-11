namespace CountriesAPI.DataLayer.Entities;

public class City : BaseEntity
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public double Area { get; set; }

    public int CountryId { get; set; }
    public Country Country { get; set; } = new();
}