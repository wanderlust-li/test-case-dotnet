namespace TestCase.Application.DTO;

public class TransactionDTO
{
    public string TransactionId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TimeZone { get; set; }
}