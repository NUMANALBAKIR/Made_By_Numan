﻿using System.ComponentModel.DataAnnotations;

namespace Client.Models.Bank;

public class TransactionDTO
{
    [Key]
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}
