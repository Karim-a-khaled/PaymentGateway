namespace PaymentGateway.Entities.DTOs;

public class GenerateBillDto
{
    public string BillAccount { get; set; }
    public DateTimeOffset? BillCycle { get; set; } 
    public decimal Amount { get; set; }
    public DateTimeOffset DueTo { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public string ReferenceInfo { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string Description { get; set; }
    public string PaymentOptions { get; set; } = null;
    public List<RevenueTypeEntryDto> RevenueTypeEntries { get; set; }
    public List<BillSummary> BillSummaries { get; set; }
}
