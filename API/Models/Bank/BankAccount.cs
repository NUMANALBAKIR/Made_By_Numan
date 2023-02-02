using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Bank;

public class BankAccount
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }
    public string HolderName { get; set; }
    public double SavingsBalance { get; set; }
    public double CheckingsBalance { get; set; }
    public double TransactionAmount { get; set; }
}
