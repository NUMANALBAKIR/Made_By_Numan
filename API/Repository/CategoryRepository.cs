using API.Data;
using API.Models.OrderFood;
using API.Repository.IRepository;

namespace API.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _db;
    public CategoryRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        _db.Categories.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
