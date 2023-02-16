using API.Data;
using API.Models.Bank;
using API.Repository.IRepository;

namespace API.Repository;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly AppDbContext _db;
    public TransactionRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Transaction> UpdateAsync(Transaction entity)
    {
        _db.Transactions.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}