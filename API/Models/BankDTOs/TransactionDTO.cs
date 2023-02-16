﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.BankDTOs;

public class TransactionDTO
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}
