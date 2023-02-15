namespace Client.Models.OrderFoodDTOs;

public class OrderDetailCreateDTO
{
    public int OrderHeaderId { get; set; }

    public int FoodId { get; set; }

    public int Count { get; set; }
    public decimal PurchasePrice { get; set; }
}
