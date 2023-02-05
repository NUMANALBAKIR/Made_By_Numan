using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.OrderFood;

public class CartItem
{
    // add AppUser

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartItemId { get; set; }

    public int FoodId { get; set; }
    [ValidateNever, ForeignKey(nameof(FoodId))]
    public Food Food { get; set; }

    public double CurrentPrice { get; set; }
    public int Count { get; set; }
}
