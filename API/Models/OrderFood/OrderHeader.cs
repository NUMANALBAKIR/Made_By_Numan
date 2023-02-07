using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.OrderFood;

public class OrderHeader
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderHeaderId { get; set; }

    public double OrderTotal { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public string OrdererName { get; set; }
    public string DeliveryAddress { get; set; }
}
