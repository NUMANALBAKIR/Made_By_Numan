﻿
namespace API.Models.OrderFoodDTOs;

public class CartItemUpdateDTO
{
    public int CartItemId { get; set; }
    public string AppUserId { get; set; }
    public int FoodId { get; set; }
    public decimal CurrentPrice { get; set; }
    public int Count { get; set; }
}
