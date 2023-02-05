
namespace Client.Models.OrderFoodDTOs;

public class CartItemUpdateDTO
{
    // add AppUser

    public int CartItemId { get; set; }

    public int FoodId { get; set; }

    public double CurrentPrice { get; set; }
    public int Count { get; set; }
}
