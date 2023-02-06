using Client.Models.OrderFood;
using Client.Models.OrderFoodDTOs;

namespace Client.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<CartItemDTO> ItemsList { get; set; }
    public OrderHeader OrderHeader { get; set; }
}
