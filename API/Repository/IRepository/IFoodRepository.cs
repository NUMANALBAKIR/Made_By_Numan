using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface IFoodRepository : IRepository<Food>
{
    Task<Food> UpdateAsync(Food entity);
}
