using InvoiceImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvoiceImporter.Infrastructure
{
    public class InvoiceDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=10.0.1.199;Database=Guidelines;Integrated Security=true;");
        }
    }
}
