using System.ComponentModel.DataAnnotations;

namespace CountriesAPI.DataLayer.Entities;

public abstract class BaseEntity : IdModel
{
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set;}
    [Required]
    public bool IsDeleted { get; set; }
}