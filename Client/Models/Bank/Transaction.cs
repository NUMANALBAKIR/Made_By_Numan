namespace Client.Models.Bank;

public class Transaction
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
