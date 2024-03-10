using Microsoft.EntityFrameworkCore;
using TestCase.Domain.Entities;

namespace TestCase.Infrastructure.DatabaseContext;

public class TestCaseContext : DbContext
{
    public TestCaseContext(DbContextOptions<TestCaseContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.OwnsOne(transaction => transaction.ClientLocation, location =>
            {
                location.Property(l => l.Latitude).HasColumnName("Latitude");
                location.Property(l => l.Longitude).HasColumnName("Longitude");
            });
        });
    }
}