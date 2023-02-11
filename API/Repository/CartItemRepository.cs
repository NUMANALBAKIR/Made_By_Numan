using API.Data;
using API.Models.OrderFood;
using API.Repository.IRepository;

namespace API.Repository;

public class CartItemRepository : Repository<CartItem>, ICartItemRepository
{
    private readonly AppDbContext _db;
    public CartItemRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<CartItem> UpdateAsync(CartItem entity)
    {
        _db.CartItems.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public int DecrementCount(CartItem cartItem, int count)
    {
        cartItem.Count -= count;
        return cartItem.Count;
    }

    public int IncrementCount(CartItem cartItem, int count)
    {
        cartItem.Count += count;
        return cartItem.Count;
    }

}
