using System.ComponentModel.DataAnnotations;

namespace API.Models.OrderFoodDTOs;

public class CartItemCreateDTO
{
    public string AppUserId { get; set; }
    public int FoodId { get; set; }
    [Precision(18, 2)] public decimal CurrentPrice { get; set; }
    [Required]
    public int Count { get; set; }
}
