using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.OrderFood;

public class OrderDetail
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderDetailId { get; set; }

    public int OrderHeaderId { get; set; }
    [ForeignKey(nameof(OrderHeaderId)), ValidateNever]
    public OrderHeader OrderHeader { get; set; }

    public int FoodId { get; set; }
    [ForeignKey(nameof(FoodId)), ValidateNever]
    public Food Food { get; set; }

    public int Count { get; set; }
    public decimal PurchasePrice { get; set; }
}
