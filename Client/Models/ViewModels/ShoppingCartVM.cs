using Client.Models.OrderFood;
using Client.Models.OrderFoodDTOs;

namespace Client.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<CartItemDTO> CartItems { get; set; }
    public OrderHeader OrderHeader { get; set; }
}
