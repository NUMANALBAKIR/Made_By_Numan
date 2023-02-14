using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.BankDTOs;

public class BankAccountDTO
{
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }
    [ValidateNever, ForeignKey(nameof(AppUserId))]
    public AppUser AppUser { get; set; }

    public string HolderName { get; set; }
    public double SavingsBalance { get; set; }
    public double CheckingsBalance { get; set; }

    [Range(1, 500, ErrorMessage = "Amount must be between 1 and 500.")]
    public double TransactionAmount { get; set; }
}
