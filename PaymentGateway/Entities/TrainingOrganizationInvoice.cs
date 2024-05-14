namespace PaymentGateway.Entities;

public class TrainingOrganizationInvoice
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public int TrainingOrganizationId { get; set; }
    public ICollection<Bill> Bills { get; set; }
}