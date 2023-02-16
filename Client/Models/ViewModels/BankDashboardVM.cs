using Client.Models.BankDTOs;

namespace Client.Models.ViewModels;

public class BankDashboardVM
{
    public BankAccountDTO BankAccount { get; set; }
    public List<TransactionDTO> Transactions { get; set; }
}
