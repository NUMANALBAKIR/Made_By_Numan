﻿namespace API.Models.OrderFoodDTOs;

public class OrderHeaderCreateDTO
{
    public string AppUserId { get; set; }

    [Precision(18, 2)] public decimal OrderTotal { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public string OrdererName { get; set; }
    public string DeliveryAddress { get; set; }
    public string EmailAddress { get; set; }

}
