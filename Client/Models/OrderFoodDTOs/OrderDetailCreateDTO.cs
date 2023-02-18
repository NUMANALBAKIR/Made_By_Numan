namespace Client.Models.OrderFoodDTOs;

public class OrderDetailCreateDTO
{
    public int OrderHeaderId { get; set; }

    public int FoodId { get; set; }

    public int Count { get; set; }
    [Precision(18, 2)] public decimal PurchasePrice { get; set; }
}
