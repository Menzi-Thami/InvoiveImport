// InvoiceRepository.cs
using InvoiceImporter.Domain;
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
            return _context.InvoiceHeaders.Any(h => h.InvoiceNumber == invoiceNumber);
        }

        public void AddInvoice(InvoiceHeader invoice)
        {
            _context.InvoiceHeaders.Add(invoice);
            _context.InvoiceLines.AddRange(invoice.Lines);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
