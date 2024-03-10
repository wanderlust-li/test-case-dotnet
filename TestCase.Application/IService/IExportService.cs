namespace TestCase.Application.IService;

public interface IExportService
{
    Task<byte[]> ExportTransactionsToExcelAsync(CancellationToken ct);
}