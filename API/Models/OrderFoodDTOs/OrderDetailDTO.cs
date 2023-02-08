using API.Models.OrderFood;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.OrderFoodDTOs;

public class OrderDetailDTO
{
    public int OrderDetailId { get; set; }

    public int OrderHeaderId { get; set; }
    public OrderHeader OrderHeader { get; set; }

    public int FoodId { get; set; }
    public Food Food { get; set; }

    public int Count { get; set; }
    public double PurchasePrice { get; set; }
}
