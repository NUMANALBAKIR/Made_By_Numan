using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class FoodCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public string? ImageURL { get; set; }


    [Required]
    public int CategoryId { get; set; }
}
