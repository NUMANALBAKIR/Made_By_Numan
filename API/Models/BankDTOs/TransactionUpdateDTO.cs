using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Bank;

public class TransactionUpdateDTO
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }

}
