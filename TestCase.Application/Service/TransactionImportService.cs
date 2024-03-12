using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using GeoTimeZone;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NodaTime;
using TestCase.Application.Exceptions;
using TestCase.Application.IService;
using TestCase.Domain.Entities;


namespace TestCase.Application.Service;

public class TransactionImportService : ITransactionImportService
{
    private readonly string _connectionString;

    public TransactionImportService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Transaction>> ProcessTransactionsFromCSVAsync(Stream csvStream)
    {
        var transactions = new List<Transaction>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };

        using (var streamReader = new StreamReader(csvStream))
        using (var csvReader = new CsvReader(streamReader, config))
        {
            csvReader.Read();
            csvReader.ReadHeader();
            string[] requiredHeaders =
                { "transaction_id", "name", "email", "amount", "transaction_date", "client_location" };
            foreach (var header in requiredHeaders)
            {
                if (!csvReader.HeaderRecord.Any(h => string.Equals(h, header, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new BadRequestException($"The required header '{header}' is missing.");
                }
            }

            csvReader.Context.RegisterClassMap<TransactionMap>();

            while (await csvReader.ReadAsync())
            {
                var transaction = csvReader.GetRecord<Transaction>();

                var timeZoneId = TimeZoneLookup
                    .GetTimeZone(transaction.ClientLocation.Latitude, transaction.ClientLocation.Longitude).Result;
                // var dateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZoneId);
                
                transaction.TimeZone = timeZoneId;

                using (var db = new SqlConnection(_connectionString))
                {
                    var existingTransaction = await db.QuerySingleOrDefaultAsync<Transaction>(
                        "SELECT * FROM Transactions WHERE TransactionId = @TransactionId",
                        new { transaction.TransactionId });

                    if (existingTransaction != null)
                    {
                        await db.ExecuteAsync(
                            "UPDATE Transactions SET Name = @Name, Email = @Email, Amount = @Amount, TransactionDate = @TransactionDate, Latitude = @Latitude, Longitude = @Longitude, TimeZone = @TimeZone WHERE TransactionId = @TransactionId",
                            new
                            {
                                transaction.Name, transaction.Email, transaction.Amount,
                                transaction.TransactionDate, Latitude = transaction.ClientLocation.Latitude,
                                Longitude = transaction.ClientLocation.Longitude, transaction.TimeZone,
                                transaction.TransactionId
                            });
                    }
                    else
                    {
                        await db.ExecuteAsync(
                            "INSERT INTO Transactions (TransactionId, Name, Email, Amount, TransactionDate, Latitude, Longitude, TimeZone) VALUES (@TransactionId, @Name, @Email, @Amount, @TransactionDate, @Latitude, @Longitude, @TimeZone)",
                            new
                            {
                                transaction.TransactionId, transaction.Name, transaction.Email, transaction.Amount,
                                transaction.TransactionDate, Latitude = transaction.ClientLocation.Latitude,
                                Longitude = transaction.ClientLocation.Longitude, transaction.TimeZone
                            });
                    }
                }

                transactions.Add(transaction);
            }
        }

        return transactions;
    }
}