namespace API.Repository.IRepository;

public interface IUnitOfWork
{
    IBankAccountRepository BankAccountRepo { get; }

    IFoodRepository FoodRepo { get; }
    ICategoryRepository CategoryRepo { get; }
    ICartItemRepository CartItemRepo { get; }
    IOrderHeaderRepository OrderHeaderRepo { get; }
    IOrderDetailRepository OrderDetailRepo { get; }

}
