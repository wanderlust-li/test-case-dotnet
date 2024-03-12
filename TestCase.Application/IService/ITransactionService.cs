using TestCase.Application.DTO;

namespace TestCase.Application.IService;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDTO>> GetTransactionsFor2023InUserTimeZone();
}