namespace TestCase.Application.IService;

public interface ITransactionExportService
{
    Task<byte[]> ExportTransactionsToExcelAsync(CancellationToken ct);
}