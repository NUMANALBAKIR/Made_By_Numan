using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class CategoryUpdateDTO
{
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; }
}
