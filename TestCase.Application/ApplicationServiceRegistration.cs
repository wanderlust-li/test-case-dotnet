using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCase.Application.IService;
using TestCase.Application.Service;

namespace TestCase.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ITransactionImportService, TransactionImportService>();
        services.AddScoped<ITransactionExportService, TransactionExportService>();
        services.AddScoped<ITransactionService, TransactionService>();
        
        return services;
    }
}