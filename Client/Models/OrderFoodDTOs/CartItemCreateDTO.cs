using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class CartItemCreateDTO
{
    public string AppUserId { get; set; }
    public int FoodId { get; set; }
    public decimal CurrentPrice { get; set; }
    [Required]
    public int Count { get; set; }
}
