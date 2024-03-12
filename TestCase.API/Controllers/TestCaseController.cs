using System.Collections;
using Dapper;
using GeoTimeZone;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using TestCase.Application.DTO;
using TestCase.Application.IService;

namespace TestCase.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestCaseController : ControllerBase
{
    private readonly ITransactionImportService _transactionImportService;
    private readonly ITransactionExportService _transactionExportService;
    private readonly ITransactionService _transactionService;

    public TestCaseController(ITransactionImportService transactionImportService, ITransactionExportService transactionExportService,
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
    public async Task<ActionResult> ExportTransactionsToExcel(CancellationToken ct)
    {
        var file = await _transactionExportService.ExportTransactionsToExcelAsync(ct);
        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transactions.xlsx");
    }
    
    
    [HttpGet("transactions/2023-user")] // task 4
    public async Task<ActionResult> GetTransactionsFor2023InUserTimeZone()
    { 
        return Ok(await _transactionService.GetTransactionsFor2023InUserTimeZone());
    }
    

}