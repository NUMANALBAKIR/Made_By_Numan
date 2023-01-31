using System.ComponentModel.DataAnnotations;

namespace API.Models.OrderFoodDTOs;

public class CategoryCreateDTO
{
    [Required]
    public string Name { get; set; }
}
