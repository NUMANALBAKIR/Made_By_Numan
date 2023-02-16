using API.Models.Bank;

namespace API.Repository.IRepository;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<Transaction> UpdateAsync(Transaction entity);
}
