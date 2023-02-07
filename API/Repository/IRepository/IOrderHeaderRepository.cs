using API.Models.OrderFood;

namespace API.Repository.IRepository;

public interface IOrderHeaderRepository : IRepository<OrderHeader>
{
    Task<OrderHeader> UpdateAsync(OrderHeader entity);
}
