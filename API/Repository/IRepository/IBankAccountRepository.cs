using API.Models.Bank;

namespace API.Repository.IRepository;

public interface IBankAccountRepository : IRepository<BankAccount>
{
    Task<BankAccount> UpdateAsync(BankAccount entity);
}
