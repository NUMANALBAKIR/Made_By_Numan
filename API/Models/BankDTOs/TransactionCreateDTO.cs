using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Bank;

public class TransactionCreateDTO
{
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal PreviousCheckingsBalance { get; set; }
    public decimal PreviousSavingsBalance { get; set; }

}
