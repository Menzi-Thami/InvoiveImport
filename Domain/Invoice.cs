using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceImporter.Domain
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Address { get; set; }
        private readonly List<InvoiceLine> _lines = new List<InvoiceLine>();

        public IReadOnlyList<InvoiceLine> Lines => _lines;

        public void AddLine(InvoiceLine line)
        {
            _lines.Add(line);
        }

        public double CalculateTotalAmount()
        {
            return _lines.Sum(line => line.CalculateLineTotal());
        }
    }
}
