
namespace Client.Models.OrderFoodDTOs;

public class CartItemUpdateDTO
{
    public int CartItemId { get; set; }
    public string AppUserId { get; set; }
    public int FoodId { get; set; }
    public double CurrentPrice { get; set; }
    public int Count { get; set; }
}
