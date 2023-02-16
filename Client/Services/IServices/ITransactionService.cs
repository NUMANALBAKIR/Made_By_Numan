using Client.Models.Bank;

namespace Client.Services.IServices;

public interface ITransactionService
{
    Task<T> GetAllAsync<T>(string appUserId, string token);
    Task<T> GetAsync<T>(int id, string token);
    Task<T> CreateAsync<T>(TransactionCreateDTO dto, string token);
    Task<T> UpdateAsync<T>(TransactionUpdateDTO dto, string token);
    Task<T> DeleteAsync<T>(int id, string token);
}
