using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.Bank;

public class BankAccount
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }

    public string HolderName { get; set; }
    public double SavingsBalance { get; set; }
    public double CheckingsBalance { get; set; }
    public double TransactionAmount { get; set; }


}
