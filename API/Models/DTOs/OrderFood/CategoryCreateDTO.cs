using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.OrderFood;

public class CategoryCreateDTO
{
    [Required]
    public string Name { get; set; }
}
