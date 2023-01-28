using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.Bank;

public class BankAccount
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }
    public string HolderName { get; set; }
    public double SavingsAccount { get; set; }
    public double CheckingsAccount { get; set; }
    public double TransactionAmount { get; set; }
}
