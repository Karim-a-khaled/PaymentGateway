using PaymentGateway.Entities.DTOs;

namespace PaymentGateway.Entities;

public class Refund
{
    public int BillId { get; set; }
    public string RefundId { get; set; }
    public string ECollectionRefundId { get; set; }
    public List<string> Items { get; set; }
    public List<int> ItemsElementName { get; set; }
    public decimal RefundAmt { get; set; }
    public DateTime RefundDt { get; set; }
    public string ECollectionRefundMethod { get; set; } = null;
    public string RefundRefInfo { get; set; }
    public string ActionReason { get; set; } = null;
    public List<RevenueEntryList> RevenueEntryList { get; set; }
}
