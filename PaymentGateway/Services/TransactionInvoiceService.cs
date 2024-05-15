using Azure;
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
    private readonly ILogger<TransactionInvoiceService> _logger;

    public TransactionInvoiceService(AppDbContext context,AccountUtilities accountUtilities, ILogger<TransactionInvoiceService> logger)
    {
        _context = context;
        _accountUtilities = accountUtilities;
        _logger = logger;
    }

    public async Task<string> LoginWithHttpClient(LoginDto loginDto) // Inject ILogger
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonPayload = JsonConvert.SerializeObject(loginDto);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/api/account/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Login failed with status code: {response.StatusCode}"); // Log error with details
                    throw new Exception($"Login failed with status code: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<User>(responseContent);

                return loginResponse.Token;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error during login for user: {loginDto.Username}"); // Log exception with context
            throw;
        }
    }

    public async Task<string> Pay(GenerateBillDto generateBillDto) // Inject ILogger
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonPayload = JsonConvert.SerializeObject(generateBillDto);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("/api/bill/generate", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error generating bill with status code: {response.StatusCode}"); 
                    throw new Exception($"Error generating bill with status code: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var billResponse = JsonConvert.DeserializeObject<Bill>(responseContent);

                return billResponse.Id.ToString();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error during payment for bill generation with data: {generateBillDto}"); 
            throw; 
        }
    }

    public async Task<string> Refund(RefundDto refundDto) // Inject ILogger
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var jsonPayload = JsonConvert.SerializeObject(refundDto);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("/api/refund/generate", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error generating refund with status code: {response.StatusCode}"); 
                    throw new Exception($"Error generating refund with status code: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var refundResponse = JsonConvert.DeserializeObject<Refund>(responseContent); 

                return refundResponse.RefundId.ToString();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error during refund for data: {refundDto.BillId}"); 
            throw new Exception($"Error generating refund with status code: {refundDto.BillId}"); ; 
        }
    }
}