using API.Data;
using API.Models.OrderFood;
using API.Repository.IRepository;

namespace API.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly AppDbContext _db;
    public OrderHeaderRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<OrderHeader> UpdateAsync(OrderHeader entity)
    {
        _db.OrderHeaders.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}