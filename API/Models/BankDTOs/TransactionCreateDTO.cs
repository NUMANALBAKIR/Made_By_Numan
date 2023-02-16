using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Bank;

public class TransactionCreateDTO
{
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }

}
