using Microsoft.EntityFrameworkCore;
using PaymentGateway.Entities;

namespace PaymentGateway.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Bill> TransactionInvoices { get; set; }
    public DbSet<TrainingOrganizationInvoice> TrainingOrganizationInvoices { get; set; }
}