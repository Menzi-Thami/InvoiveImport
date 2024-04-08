using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoiceImporter.Domain
{
    public class InvoiceLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineId { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public double? Quantity { get; set; }
        public double? UnitSellingPriceExVAT { get; set; }
        public InvoiceHeader Invoice { get; set; } 
    }
}
