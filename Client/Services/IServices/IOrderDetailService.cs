using Client.Models.OrderFoodDTOs;

namespace Client.Services.IServices;

public interface IOrderDetailService
{
    Task<T> GetAllAsync<T>(string token);
    Task<T> GetAsync<T>(int id, string token);
    Task<T> CreateAsync<T>(OrderDetailCreateDTO dto, string token);
    Task<T> UpdateAsync<T>(OrderDetailUpdateDTO dto, string token);
    Task<T> DeleteAsync<T>(int id, string token);
}
