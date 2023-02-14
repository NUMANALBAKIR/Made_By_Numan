using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
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
    [Precision(18, 2)]
    public double SavingsBalance { get; set; }
    [Precision(18, 2)]
    public double CheckingsBalance { get; set; }

    [Precision(18, 2)]
    [Range(1, 500, ErrorMessage = "Amount must be between 1 and 500.")]
    public double TransactionAmount { get; set; }
}
