namespace Client.Models.OrderFoodDTOs;

public class OrderDetailUpdateDTO
{
    public int OrderDetailId { get; set; }

    public int OrderHeaderId { get; set; }

    public int FoodId { get; set; }

    public int Count { get; set; }
    public double PurchasePrice { get; set; }
}
