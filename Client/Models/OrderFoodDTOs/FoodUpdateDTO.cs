using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class FoodUpdateDTO
{
    [Required]
    public int FoodId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    public string? ImageURL { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
