using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class CartItemCreateDTO
{
    // add AppUser

    public int FoodId { get; set; }

    public double CurrentPrice { get; set; }
    [Required]
    public int Count { get; set; }
}
