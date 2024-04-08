using InvoiceImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvoiceImporter.Infrastructure
{
    public class InvoiceDbContext : DbContext
    {
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=10.0.1.199;Database=Guidelines;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceHeader>(entity =>
            {
                entity.ToTable("InvoiceHeader");
                entity.HasKey(e => e.InvoiceId);
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.ToTable("InvoiceLines");
                entity.HasKey(e => e.LineId);

                entity.HasOne(e => e.Invoice)
                      .WithMany(e => e.Lines)
                      .HasForeignKey(il => il.InvoiceId)
                      .IsRequired();
            });
        }
    }
}
