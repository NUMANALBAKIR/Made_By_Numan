namespace API.Models.BankDTOs;

public class BankAccountCreateDTO
{
    public string HolderName { get; set; }
    public double SavingsBalance { get; set; }
    public double CheckingsBalance { get; set; }
    public double TransactionAmount { get; set; }
}

