
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Client.Models.OrderFoodDTOs;

public class CartItemCreateDTO
{
    // add AppUser

    public int FoodId { get; set; }
    [ValidateNever]
    public FoodDTO Food { get; set; }

    public double CurrentPrice { get; set; }
    [Required]
    public int Count { get; set; }
}
