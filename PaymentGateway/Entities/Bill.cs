namespace PaymentGateway.Entities;

public class Bill
{
    public int Id { get; set; }
    public string BillAccount { get; set; }
    public DateTimeOffset? BillCycle { get; set; } // nullable DateTimeOffset
    public decimal Amount { get; set; }
    public DateTimeOffset DueTo { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public string ReferenceInfo { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string Description { get; set; }
    public string? PaymentOptions { get; set; } = null;
    public List<RevenueTypeEntry> RevenueTypeEntries { get; set; }
    public List<BillSummary> BillSummaries { get; set; }
}