using System.ComponentModel.DataAnnotations;

namespace CountriesAPI.DataLayer.Entities;

public abstract class IdModel
{
    [Key, Required]
    public int Id { get; set; }
}