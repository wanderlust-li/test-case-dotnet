namespace TestCase.Domain.Entities;

public class Transaction
{
    public string TransactionId { get; set; } 
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public double Amount { get; set; }
    
    public DateTime TransactionDate { get; set; }
    
    public Location ClientLocation { get; set; }
    
    // TimeZone represents the time zone obtained from ClientLocation coordinates
    public string? TimeZone { get; set; } 
}