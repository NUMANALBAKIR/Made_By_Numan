using API.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Bank;

public class BankAccount
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }

    public string HolderName { get; set; }
    [Precision(18, 2)]
    public double SavingsBalance { get; set; }
    [Precision(18, 2)]
    public double CheckingsBalance { get; set; }
    [Precision(18, 2)]
    public double TransactionAmount { get; set; }
}
