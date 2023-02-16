using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models.Bank;

public class TransactionUpdateDTO
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal PreviousCheckingsBalance { get; set; }
    public decimal PreviousSavingsBalance { get; set; }

}
