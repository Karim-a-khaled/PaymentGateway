namespace PaymentGateway.Entities;

public class RevenueTypeEntry
{
    public int Id { get; set; }
    public string BeneficiaryAgencyId { get; set; }
    public string GfsCode { get; set; }
    public decimal Amount { get; set; }
}