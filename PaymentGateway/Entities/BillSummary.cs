namespace PaymentGateway.Entities;

public class BillSummary
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int AmountCode { get; set; } 
}