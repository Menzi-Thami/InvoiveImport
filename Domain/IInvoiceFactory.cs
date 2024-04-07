namespace InvoiceImporter.Domain
{
    public interface IInvoiceFactory
    {
        Invoice CreateInvoice(string[] csvRow);
    }
}
