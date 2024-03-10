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
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddScoped<IExportService, ExportService>();
        
        return services;
    }
}