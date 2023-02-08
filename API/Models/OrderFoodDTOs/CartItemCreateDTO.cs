namespace API.Models.OrderFoodDTOs;

public class CartItemCreateDTO
{
    // add AppUser

    public int FoodId { get; set; }

    public double CurrentPrice { get; set; }
    public int Count { get; set; }
}
