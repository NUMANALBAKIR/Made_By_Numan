﻿using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

    public double CurrentPrice { get; set; }
    public int Count { get; set; }
}
