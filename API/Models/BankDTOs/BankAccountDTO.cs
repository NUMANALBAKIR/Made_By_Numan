﻿namespace API.Models.BankDTOs;

public class BankAccountDTO
{
    public int AccountId { get; set; }
    public string HolderName { get; set; }
    public double SavingsBalance { get; set; }
    public double CheckingsBalance { get; set; }
    public double TransactionAmount { get; set; }
}
