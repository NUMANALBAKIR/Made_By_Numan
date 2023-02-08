using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.OrderFoodDTOs;

public class OrderHeaderCreateDTO
{
    public double OrderTotal { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public string OrdererName { get; set; }
    public string DeliveryAddress { get; set; }
}
