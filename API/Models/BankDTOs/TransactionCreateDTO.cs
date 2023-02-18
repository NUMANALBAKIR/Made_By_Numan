namespace API.Models.Bank;

public class TransactionCreateDTO
{
    public string AppUserId { get; set; }
    public string Message { get; set; }
    public DateTime TransactionDate { get; set; }
    [Precision(18, 2)] public decimal PreviousCheckingsBalance { get; set; }
    [Precision(18, 2)] public decimal PreviousSavingsBalance { get; set; }

}
