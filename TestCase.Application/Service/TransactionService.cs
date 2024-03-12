using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TestCase.Application.DTO;
using TestCase.Application.IService;

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
        var myTimeZone = TimeZoneInfo.Local;
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(myTimeZone.Id);
        Console.WriteLine(timeZone.Id);
    
        using (var connection = new SqlConnection(_connectionString))
        {
            var transactions = await connection.QueryAsync<TransactionDTO>(
                @"SELECT * FROM Transactions 
              WHERE YEAR(TransactionDate) = 2023 AND TimeZone = @ServerTimeZone",
                new { ServerTimeZone = timeZone.Id });

            return transactions; 
        }
    }

}