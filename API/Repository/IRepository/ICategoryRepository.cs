using API.Models;

namespace API.Repository.IRepository;

public interface ICategoryRepository
{
    Task<Category> UpdateAsync(Category entity);
}
