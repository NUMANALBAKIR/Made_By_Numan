using Client.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.BankDTOs;

public class BankAccountUpdateDTO
{
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }

    public string HolderName { get; set; }
    public decimal SavingsBalance { get; set; }
    public decimal CheckingsBalance { get; set; }
    public decimal TransactionAmount { get; set; }
}

