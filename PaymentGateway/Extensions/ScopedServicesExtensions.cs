using PaymentGateway.Interfaces;
using PaymentGateway.Services;
using PaymentGateway.Services.Utilities;

namespace PaymentGateway.Extensions;

public static class ScopedServicesExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionInvoiceService, TransactionInvoiceService>();
        services.AddScoped<AccountUtilities>();

        return services;
    }
}