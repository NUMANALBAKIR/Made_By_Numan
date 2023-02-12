using Client.Models.OrderFoodDTOs;

namespace Client.Services.IServices;

public interface ICartItemService
{
    Task<T> GetAllAsync<T>(string appUserId, string token);
    Task<T> GetAsync<T>(int id, string appUserId, string token);
    Task<T> CreateAsync<T>(CartItemCreateDTO dto, string token);
    Task<T> UpdateAsync<T>(CartItemUpdateDTO dto, string token);
    Task<T> DeleteAsync<T>(int id, string token);
}
