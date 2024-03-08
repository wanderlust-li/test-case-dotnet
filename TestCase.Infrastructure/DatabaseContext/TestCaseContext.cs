using Microsoft.EntityFrameworkCore;
using TestCase.Domain.Entities;

namespace TestCase.Infrastructure.DatabaseContext;

public class TestCaseContext : DbContext
{
    public TestCaseContext(DbContextOptions<TestCaseContext> options) : base(options)
    {
        
    }

    public DbSet<Transaction> Transactions;
    
}