using InvoiceImporter.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceImporter.Infrastructure.Configurations
{
    /// <summary>
    /// EF Core mapping for <see cref="InvoiceLine"/>.
    /// </summary>
    public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> entity)
        {
            entity.ToTable("InvoiceLines");

            entity.HasKey(e => e.LineId);
            entity.Property(e => e.LineId).ValueGeneratedOnAdd();

            entity.Property(e => e.Description);
            entity.Property(e => e.Quantity);
            entity.Property(e => e.UnitSellingPriceExVAT);
        }
    }
}
