using TestCase.Application.DTO;

namespace TestCase.Application.IService;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDTO>> GetTransactionsFor2023InUserTimeZone();

    Task<IEnumerable<TransactionDTO>> GetTransactionsFor2023();

    Task<IEnumerable<TransactionDTO>> GetTransactionsForJanuary2024InUserTimeZone();

    Task<IEnumerable<TransactionDTO>> GetTransactionsForJanuary2024();
}