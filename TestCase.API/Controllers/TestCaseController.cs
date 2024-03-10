using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestCase.Application.DTO;
using TestCase.Application.Exceptions;
using TestCase.Application.Helpers;
using TestCase.Application.IService;

namespace TestCase.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestCaseController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IExportService _exportService;

    public TestCaseController(ITransactionService transactionService, IExportService exportService)
    {
        _transactionService = transactionService;
        _exportService = exportService;
    }

    [HttpPost("read-transactions-csv")]
    public async Task<IActionResult> PostTransactionsFromCSV([FromForm] IFormFileCollection file)
    {
        return Ok(await _transactionService.ProcessTransactionsFromCSVAsync(file[0].OpenReadStream()));
    }
    
    [HttpGet("export-excel-file-transactions")]
    public async Task<ActionResult> Download(CancellationToken ct)
    {
        var file = await _exportService.ExportTransactionsToExcelAsync(ct);
        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transactions.xlsx");
    }
    
}