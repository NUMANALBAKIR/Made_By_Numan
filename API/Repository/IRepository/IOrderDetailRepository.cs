using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    Task<OrderDetail> UpdateAsync(OrderDetail entity);
}
