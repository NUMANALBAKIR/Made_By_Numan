namespace Client.Models.BankDTOs;

public class BankAccountUpdateDTO
{
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }

    public string HolderName { get; set; }
    public decimal SavingsBalance { get; set; }
    public decimal CheckingsBalance { get; set; }
    public decimal TransactionAmount { get; set; }
}

