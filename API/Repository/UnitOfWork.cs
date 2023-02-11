using API.Data;
using API.Repository.IRepository;

namespace API.Repository;

public class UnitOfWork : IUnitOfWork
{
    public IFoodRepository FoodRepo { get; private set; }
    public ICategoryRepository CategoryRepo { get; private set; }
    public IBankAccountRepository BankAccountRepo { get; private set; }
    public ICartItemRepository CartItemRepo { get; private set; }
    public IOrderHeaderRepository OrderHeaderRepo { get; private set; }
    public IOrderDetailRepository OrderDetailRepo { get; private set; }

    public IAppUserRepository AppUserRepo { get; private set; }




    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        FoodRepo = new FoodRepository(_db);
        CategoryRepo = new CategoryRepository(_db);
        BankAccountRepo = new BankAccountRepository(_db);
        CartItemRepo = new CartItemRepository(_db);
        OrderHeaderRepo = new OrderHeaderRepository(_db);
        OrderDetailRepo = new OrderDetailRepository(_db);
        AppUserRepo = new AppUserRepository(_db);

    }
}
