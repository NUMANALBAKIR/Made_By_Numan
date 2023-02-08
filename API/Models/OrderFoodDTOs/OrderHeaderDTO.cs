namespace API.Models.OrderFoodDTOs;

public class OrderHeaderDTO
{
    public int OrderHeaderId { get; set; }

    public double OrderTotal { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public string OrdererName { get; set; }
    public string DeliveryAddress { get; set; }
}
