﻿using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.OrderFoodDTOs;

public class OrderHeaderDTO
{
    public int OrderHeaderId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUserDTO AppUser { get; set; }

    [Precision(18, 2)] public decimal OrderTotal { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public string OrdererName { get; set; }
    public string DeliveryAddress { get; set; }
    public string EmailAddress { get; set; }

}
