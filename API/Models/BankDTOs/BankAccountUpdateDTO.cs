﻿namespace API.Models.BankDTOs;

public class BankAccountUpdateDTO
{
    public int BankAccountId { get; set; }

    public string AppUserId { get; set; }

    public string HolderName { get; set; }
    [Precision(18, 2)] public decimal SavingsBalance { get; set; }
    [Precision(18, 2)] public decimal CheckingsBalance { get; set; }
    [Precision(18, 2)] public decimal TransactionAmount { get; set; }
}

