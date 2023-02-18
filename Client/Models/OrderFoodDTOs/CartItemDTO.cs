using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.OrderFoodDTOs;

public class CartItemDTO
{
    public int CartItemId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }

    public int FoodId { get; set; }
    [ValidateNever, ForeignKey(nameof(FoodId))]
    public FoodDTO Food { get; set; }

    [Precision(18, 2)] public decimal CurrentPrice { get; set; }
    [Range(1, 20, ErrorMessage = "Items must be between 1 and 20")]
    public int Count { get; set; }
}
