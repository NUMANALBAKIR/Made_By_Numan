using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface ICartItemRepository : IRepository<CartItem>
{
    Task<CartItem> UpdateAsync(CartItem entity);
}
