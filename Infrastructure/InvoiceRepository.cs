using InvoiceImporter.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InvoiceImporter.Infrastructure
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceRepository(InvoiceDbContext context)
        {
            _context = context;
        }

        public bool InvoiceExists(string invoiceNumber)
        {
            return _context.Invoices.Any(h => h.InvoiceNumber == invoiceNumber);
        }

        public void AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.InvoiceLines.AddRange(invoice.Lines); 
        }

        public double GetInvoiceLinesTotal()
        {
            return _context.InvoiceLines.Sum(line => line.Quantity * line.UnitPrice);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
