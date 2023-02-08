using API.Models.OrderFood;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.OrderFoodDTOs;

public class OrderDetailCreateDTO
{
    public int OrderHeaderId { get; set; }

    public int FoodId { get; set; }

    public int Count { get; set; }
    public double PurchasePrice { get; set; }
}
