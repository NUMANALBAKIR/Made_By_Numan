using API.Data;
using API.Models.OrderFood;
using API.Repository.IRepository;

namespace API.Repository;

public class FoodRepository : Repository<Food>, IFoodRepository
{
    private readonly AppDbContext _db;
    public FoodRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Food> UpdateAsync(Food entity)
    {
        _db.Foods.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
