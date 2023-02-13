using API.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.BankDTOs;

public class BankAccountUpdateDTO
{
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }

    public string HolderName { get; set; }
    public double SavingsBalance { get; set; }
    public double CheckingsBalance { get; set; }
    public double TransactionAmount { get; set; }
}

