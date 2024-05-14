using Newtonsoft.Json;
using PaymentGateway.Data;
using PaymentGateway.Entities;
using PaymentGateway.Entities.DTOs;
using PaymentGateway.Interfaces;
using PaymentGateway.Services.Utilities;
using System.Text;

namespace PaymentGateway.Services;

public class TransactionInvoiceService : ITransactionInvoiceService
{
    private readonly AppDbContext _context;
    private readonly AccountUtilities _accountUtilities;
    public TransactionInvoiceService(AppDbContext context,AccountUtilities accountUtilities)
    {
        _context = context;
        _accountUtilities = accountUtilities;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _accountUtilities.CheckUser(loginDto);

        if (user is null)
            return null;

        string token = await _accountUtilities.GenerateToken(user); // use httpclient to put the tahseel token

        return token;
    }

    public async Task<string> Pay(GenerateBillDto generateBillDto)
    {
        using (var httpClient = new HttpClient())
        {
            var jsonPayload = JsonConvert.SerializeObject(generateBillDto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/bill/generate", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error generating bill: {response.StatusCode}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var billResponse = JsonConvert.DeserializeObject<Bill>(responseContent);

            return billResponse.Id.ToString();
        }
    }

    public async Task<string> Refund(RefundDto refundDto)
    {
        using (var httpClient = new HttpClient())
        {
            var jsonPayload = JsonConvert.SerializeObject(refundDto);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/refund/generate", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error generating refund: {response.StatusCode}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var refundResponse = JsonConvert.DeserializeObject<Bill>(responseContent);

            return refundResponse.Id.ToString();
        }
    }
}