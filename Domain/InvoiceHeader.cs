using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoiceImporter.Domain
{
    public class InvoiceHeader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Address { get; set; }
        public double? InvoiceTotal { get; set; }

        public List<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
    }
}
