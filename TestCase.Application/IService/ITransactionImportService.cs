using TestCase.Domain.Entities;

namespace TestCase.Application.IService;

public interface ITransactionImportService
{
    Task<IEnumerable<Transaction>> ProcessTransactionsFromCSVAsync(Stream csvStream);
}