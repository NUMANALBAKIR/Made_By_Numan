﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.BankDTOs;

public class TransactionDTO
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime TransactionDate { get; set; }
    [Precision(18, 2)] public decimal PreviousCheckingsBalance { get; set; }
    [Precision(18, 2)] public decimal PreviousSavingsBalance { get; set; }
}
