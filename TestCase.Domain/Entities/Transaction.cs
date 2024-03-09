namespace TestCase.Domain.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; } 
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public double Amount { get; set; }
    
    public DateTime TransactionDate { get; set; }
    
    public Tuple<double, double> ClientLocation { get; set; }
    
    // TimeZone represents the time zone obtained from ClientLocation coordinates
    public string TimeZone { get; set; } 
}