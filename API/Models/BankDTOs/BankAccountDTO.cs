﻿using API.Models.User;
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
    public decimal SavingsBalance { get; set; }
    public decimal CheckingsBalance { get; set; }
    [Range(1, 500, ErrorMessage = "Amount must be between 1 and 500.")]
    public decimal TransactionAmount { get; set; }
}
