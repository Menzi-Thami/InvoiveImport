using System.Reflection;
using InvoiceImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvoiceImporter.Infrastructure
{
    public class InvoiceDbContext : DbContext
    {
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; } = null!;
        public DbSet<InvoiceLine> InvoiceLines { get; set; } = null!;

        // Parameterless ctor keeps the existing design-time / console usage working.
        public InvoiceDbContext()
        {
        }

        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=10.0.1.199;Database=Guidelines;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
