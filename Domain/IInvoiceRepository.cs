namespace InvoiceImporter.Domain
{
    public interface IInvoiceRepository
    {
        bool InvoiceExists(string invoiceNumber);
        void AddInvoice(Invoice invoice);
        double GetInvoiceLinesTotal();
        void SaveChanges();
    }
}
