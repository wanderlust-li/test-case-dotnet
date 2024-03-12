using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TestCase.Application.DTO;
using TestCase.Application.Exceptions;
using TestCase.Application.Helpers;
using TestCase.Application.IService;

namespace TestCase.Application.Service;

public class TransactionExportService : ITransactionExportService
{
    private readonly string _connectionString;

    public TransactionExportService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<byte[]> ExportTransactionsToExcelAsync(CancellationToken ct)
    {
        List<TransactionDTO> transactions;

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            const string query = @"
                SELECT
                    [TransactionId],
                    [Name],
                    [Email],
                    [Amount],
                    [TransactionDate],
                    [TimeZone]
                FROM [TestCase].[dbo].[Transactions]";

            transactions = (await db.QueryAsync<TransactionDTO>(query)).ToList();
        }

        if (transactions == null || transactions.Count == 0)
        {
            throw new NotFoundException("No transactions found to export.");
        }

        return ExcelHelper.CreateFile(transactions);
    }
}