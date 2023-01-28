namespace Client.Models.Bank;

public class BankAccount
{
    public int Id { get; set; }
    public string HolderName { get; set; }
    public double SavingsAccount { get; set; }
    public double CheckingsAccount { get; set; }
    public double TransactionAmount { get; set; }
}
