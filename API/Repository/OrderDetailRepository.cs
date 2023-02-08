using API.Data;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Repository.IRepository;

namespace API.Repository;

public class OrderDetailRepository : Repository<OrderDetailCreateDTO>, IOrderDetailRepository
{
    private readonly AppDbContext _db;
    public OrderDetailRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<OrderDetailCreateDTO> UpdateAsync(OrderDetailCreateDTO entity)
    {
        _db.OrderDetails.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}