using TestCase.Domain.Entities;

namespace TestCase.Application.IService;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> ProcessTransactionsFromCSVAsync(Stream csvStream);
}