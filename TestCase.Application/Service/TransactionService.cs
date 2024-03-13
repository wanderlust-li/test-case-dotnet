using Dapper;
using GeoTimeZone;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NodaTime;
using TestCase.Application.DTO;
using TestCase.Application.IService;
using TimeZoneMapper;

namespace TestCase.Application.Service;

public class TransactionService : ITransactionService
{
    private readonly string _connectionString;

    public TransactionService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<TransactionDTO>> GetTransactionsFor2023InUserTimeZone()
    {
        var localDateTimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
        var instant = SystemClock.Instance.GetCurrentInstant();
        var localOffset = localDateTimeZone.GetUtcOffset(instant);

        List<TransactionDTO> filteredTransactions = new List<TransactionDTO>();

        using (var connection = new SqlConnection(_connectionString))
        {
            var transactions = await connection.QueryAsync<TransactionDTO>(
                @"SELECT * FROM Transactions 
              WHERE YEAR(TransactionDate) = 2023");

            foreach (var transaction in transactions)
            {
                var transactionTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(transaction.TimeZone);
                if (transactionTimeZone != null)
                {
                    var transactionOffset = transactionTimeZone.GetUtcOffset(instant);

                    if (transactionOffset == localOffset)
                    {
                        filteredTransactions.Add(transaction);
                    }
                }
            }
        }

        return filteredTransactions;
    }

    public async Task<IEnumerable<TransactionDTO>> GetTransactionsFor2023()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var transactions = await connection.QueryAsync<TransactionDTO>(
                @"SELECT * FROM Transactions 
              WHERE YEAR(TransactionDate) = 2023");

            return transactions;
        }
    }

    public async Task<IEnumerable<TransactionDTO>> GetTransactionsForJanuary2024InUserTimeZone()
    {
        var localDateTimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
        var instant = SystemClock.Instance.GetCurrentInstant();
        var localOffset = localDateTimeZone.GetUtcOffset(instant);

        List<TransactionDTO> filteredTransactions = new List<TransactionDTO>();

        using (var connection = new SqlConnection(_connectionString))
        {
            var transactions = await connection.QueryAsync<TransactionDTO>(
                @"SELECT * FROM Transactions 
            WHERE YEAR(TransactionDate) = 2024");

            foreach (var transaction in transactions)
            {
                var transactionTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(transaction.TimeZone);
                if (transactionTimeZone != null)
                {
                    var transactionOffset = transactionTimeZone.GetUtcOffset(instant);

                    if (transactionOffset == localOffset && transaction.TransactionDate.Month == 1)
                    {
                        filteredTransactions.Add(transaction);
                    }
                }
            }

            return filteredTransactions;
        }
    }
}