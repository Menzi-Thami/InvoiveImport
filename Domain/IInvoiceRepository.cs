namespace InvoiceImporter.Domain
{
    public interface IInvoiceRepository
    {
        bool InvoiceExists(string invoiceNumber);
        void AddInvoice(InvoiceHeader invoice);
        void SaveChanges();
    }
}