using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface IOrderDetailRepository : IRepository<OrderDetailCreateDTO>
{
    Task<OrderDetailCreateDTO> UpdateAsync(OrderDetailCreateDTO entity);
}
