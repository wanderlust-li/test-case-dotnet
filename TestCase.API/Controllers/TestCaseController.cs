using Microsoft.AspNetCore.Mvc;
using TestCase.Application.IService;

namespace TestCase.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestCaseController : ControllerBase
{
    private readonly ITransactionImportService _transactionImportService;
    private readonly ITransactionExportService _transactionExportService;
    private readonly ITransactionService _transactionService;

    public TestCaseController(ITransactionImportService transactionImportService,
        ITransactionExportService transactionExportService,
        ITransactionService transactionService)
    {
        _transactionImportService = transactionImportService;
        _transactionExportService = transactionExportService;
        _transactionService = transactionService;
    }

    [HttpPost("import-transactions-csv")]
    public async Task<IActionResult> ImportTransactionsFromCSV([FromForm] IFormFileCollection file)
    {
        return Ok(await _transactionImportService.ProcessTransactionsFromCSVAsync(file[0].OpenReadStream()));
    }

    [HttpGet("export-transactions-excel")]
    public async Task<IActionResult> ExportTransactionsToExcel(CancellationToken ct)
    {
        var file = await _transactionExportService.ExportTransactionsToExcelAsync(ct);
        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transactions.xlsx");
    }


    [HttpGet("transactions/2023-user-time-zone")] 
    public async Task<IActionResult> GetTransactionsFor2023InUserTimeZone()
    {
        return Ok(await _transactionService.GetTransactionsFor2023InUserTimeZone());
    }

    [HttpGet("transactions/2023")] 
    public async Task<IActionResult> GetTransactionsFor2023()
    {
        return Ok(await _transactionService.GetTransactionsFor2023());
    }

    [HttpGet("transactions/2024-january-user-time-zone")] 
    public async Task<IActionResult> GetTransactionsForJanuary2024InUserTimeZone()
    {
        return Ok(await _transactionService.GetTransactionsForJanuary2024InUserTimeZone());
    }
    
    [HttpGet("transactions/2024-january")] 
    public async Task<IActionResult> GetTransactionsForJanuary2024()
    {
        return Ok(await _transactionService.GetTransactionsForJanuary2024());
    }
}