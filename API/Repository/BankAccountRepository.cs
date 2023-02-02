using API.Data;
using API.Models.Bank;
using API.Repository.IRepository;

namespace API.Repository;

public class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
{
    private readonly AppDbContext _db;

    public BankAccountRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<BankAccount> UpdateAsync(BankAccount entity)
    {
        _db.BankAccounts.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
