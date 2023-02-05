using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;

namespace API.Repository.IRepository;

public interface ICartItemRepository : IRepository<CartItem>
{
    Task<CartItem> UpdateAsync(CartItem entity);
}
