using PaymentGateway.Entities.DTOs;

namespace PaymentGateway.Interfaces;

public interface ITransactionInvoiceService
{
    Task<string> Login(LoginDto loginDto);
    Task<string> Pay(GenerateBillDto generateBillDto);
    Task<string> Refund(RefundDto refundDto);
}