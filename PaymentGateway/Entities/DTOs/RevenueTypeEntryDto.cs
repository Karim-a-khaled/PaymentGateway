namespace PaymentGateway.Entities.DTOs;

public class RevenueTypeEntryDto
{
    public string BeneficiaryAgencyId { get; set; }
    public string GfsCode { get; set; }
    public decimal Amount { get; set; }
}
