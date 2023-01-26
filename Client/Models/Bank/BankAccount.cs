namespace Client.Models.Bank;

public class BankAccount
{
    public int Id { get; set; }
    public string HolderName { get; set; } = string.Empty;
    public double SavingsAccount { get; set; }
    public double CheckingsAccount { get; set; }
}
