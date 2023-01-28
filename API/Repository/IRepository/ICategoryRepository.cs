using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> UpdateAsync(Category entity);
}
