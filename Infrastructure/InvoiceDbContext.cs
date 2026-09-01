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
                // Connection string is resolved from configuration at runtime
                // (environment variables / user-secrets / appsettings.json) and is
                // never hardcoded here.
                optionsBuilder.UseSqlServer(InvoiceDbConfiguration.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
