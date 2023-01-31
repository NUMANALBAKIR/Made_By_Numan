using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class CategoryCreateDTO
{
    [Required]
    public string Name { get; set; }
}
