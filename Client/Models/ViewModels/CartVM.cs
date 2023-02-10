using Client.Models.OrderFood;
using Client.Models.OrderFoodDTOs;

namespace Client.Models.ViewModels;

public class CartVM
{
    public List<CartItemDTO> CartItems { get; set; }
    public OrderHeader OrderHeader { get; set; }
}
