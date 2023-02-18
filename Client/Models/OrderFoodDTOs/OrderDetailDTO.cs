using Client.Models.OrderFood;

namespace Client.Models.OrderFoodDTOs;

public class OrderDetailDTO
{
    public int OrderDetailId { get; set; }

    public int OrderHeaderId { get; set; }
    public OrderHeader OrderHeader { get; set; }

    public int FoodId { get; set; }
    public Food Food { get; set; }

    public int Count { get; set; }
    [Precision(18, 2)] public decimal PurchasePrice { get; set; }
}
