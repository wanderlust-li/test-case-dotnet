using Microsoft.AspNetCore.Mvc;
using TestCase.Application.IService;

namespace TestCase.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestCaseController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    
    public TestCaseController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("read-transactions-csv")]
    public async Task<IActionResult> PostTransactionsFromCSV([FromForm] IFormFileCollection file)
    {
        return Ok(await _transactionService.ProcessTransactionsFromCSVAsync(file[0].OpenReadStream()));
    }
    
}