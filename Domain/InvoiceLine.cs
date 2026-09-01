using System;

namespace InvoiceImporter.Domain
{
    /// <summary>
    /// A single invoice line. Plain C# domain type — no EF or CSV concerns.
    /// </summary>
    public class InvoiceLine
    {
        // Parameterless constructor for the EF Core materialiser only.
        private InvoiceLine()
        {
            Description = string.Empty;
        }

        public InvoiceLine(string description, double? quantity, double? unitSellingPriceExVat)
        {
            Description = description ?? string.Empty;
            Quantity = quantity;
            UnitSellingPriceExVAT = unitSellingPriceExVat;
        }

        public int LineId { get; private set; }
        public int InvoiceId { get; private set; }
        public string Description { get; private set; }
        public double? Quantity { get; private set; }
        public double? UnitSellingPriceExVAT { get; private set; }

        // Populated by EF Core when the parent is set / materialised.
        public InvoiceHeader? Invoice { get; private set; }
    }
}
