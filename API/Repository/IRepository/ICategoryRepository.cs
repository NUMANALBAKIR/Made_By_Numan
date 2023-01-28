using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface ICategoryRepository
{
    Task<Category> UpdateAsync(Category entity);
}
