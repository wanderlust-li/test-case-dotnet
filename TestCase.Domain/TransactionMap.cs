using System;
using System.Globalization;
using CsvHelper.Configuration;

namespace TestCase.Domain.Entities;

public sealed class TransactionMap : ClassMap<Transaction>
{
    public TransactionMap()
    {
        Map(m => m.TransactionId).Name("transaction_id");
        Map(m => m.Name).Name("name");
        Map(m => m.Email).Name("email");
        Map(m => m.Amount).Name("amount").Convert(args =>
        {
            var amountString = args.Row.GetField<string>("amount").Replace("$", "").Trim();

            if (double.TryParse(amountString, NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
            {
                return amount;
            }
            return 0; 
        });
        Map(m => m.TransactionDate).Name("transaction_date").TypeConverterOption.Format("yyyy-MM-dd HH:mm:ss");
        Map(m => m.ClientLocation.Latitude).Convert(args =>
        {
            var location = args.Row.GetField("client_location").Split(',');
            return double.TryParse(location[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double latitude) ? latitude : 0;
        });
        Map(m => m.ClientLocation.Longitude).Convert(args =>
        {
            var location = args.Row.GetField("client_location").Split(',');
            return double.TryParse(location[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double longitude) ? longitude : 0;
        });
    }
}
