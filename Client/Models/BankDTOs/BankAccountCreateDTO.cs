namespace Client.Models.BankDTOs;

public class BankAccountCreateDTO
{
    public string AppUserId { get; set; }

    public string HolderName { get; set; }
    public decimal SavingsBalance { get; set; }
    public decimal CheckingsBalance { get; set; }
    public decimal TransactionAmount { get; set; }
}

