using API.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.BankDTOs;

public class BankAccountDTO
{
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }

    public string HolderName { get; set; }
    [Precision(18, 2)] public decimal SavingsBalance { get; set; }
    [Precision(18, 2)] public decimal CheckingsBalance { get; set; }
    [Range(1, 500, ErrorMessage = "Amount must be between 1 and 500.")]
    [Precision(18, 2)] public decimal TransactionAmount { get; set; }
}
