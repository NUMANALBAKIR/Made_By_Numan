namespace API.Repository.IRepository;

public interface IUnitOfWork
{
    IFoodRepository FoodRepo { get; }
    ICategoryRepository CategoryRepo { get; }
    IBankAccountRepository BankAccountRepo { get; }
    ICartItemRepository CartItemRepo { get; }
}
