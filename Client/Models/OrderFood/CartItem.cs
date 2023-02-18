using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.OrderFood;

public class CartItem
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartItemId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }

    public int FoodId { get; set; }
    [ValidateNever, ForeignKey(nameof(FoodId))]
    public Food Food { get; set; }

    [Precision(18, 2)] public decimal CurrentPrice { get; set; }
    public int Count { get; set; }
}
