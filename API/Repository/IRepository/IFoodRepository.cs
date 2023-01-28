using API.Models;

namespace API.Repository.IRepository;

public interface IFoodRepository
{
    Task<Food> UpdateAsync(Food entity);
}
