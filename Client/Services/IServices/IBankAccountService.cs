using Client.Models.BankDTOs;

namespace Client.Services.IServices;

public interface IBankAccountService
{
    Task<T> GetAllAsync<T>(string token);
    Task<T> GetAsync<T>(string appUserId, string token);
    Task<T> CreateAsync<T>(BankAccountCreateDTO dto, string token);
    Task<T> UpdateAsync<T>(BankAccountUpdateDTO dto, string token);
    Task<T> DeleteAsync<T>(int id, string token);
}
