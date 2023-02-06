
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Client.Models.OrderFoodDTOs;

public class CartItemDTO
{
    // add AppUser

    public int CartItemId { get; set; }

    public int FoodId { get; set; }
    [ValidateNever]
    public FoodDTO Food { get; set; }

    public double CurrentPrice { get; set; }
    public int Count { get; set; }
}
