

using TestCase.Domain.Entities;

namespace TestCase.Application.DTO;

public class TransactionDTO
{
    public string TransactionId { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public double ClientLocationLatitude { get; set; }
    public double ClientLocationLongitude { get; set; }
    public string TimeZone { get; set; } 

    // Метод для конвертації DTO в доменну модель
    public Transaction ToDomainModel()
    {
        return new Transaction
        {
            TransactionId = this.TransactionId,
            Name = this.Name,
            Email = this.Email,
            Amount = this.Amount,
            TransactionDate = this.TransactionDate,
            // Припустимо, що Location є типом, який ви хочете використовувати для зберігання геолокації
            ClientLocation = new Location
            {
                Latitude = this.ClientLocationLatitude,
                Longitude = this.ClientLocationLongitude
            },
            TimeZone = this.TimeZone
        };
    }
}