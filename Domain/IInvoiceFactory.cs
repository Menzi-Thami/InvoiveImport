namespace InvoiceImporter.Domain
{
    public interface IInvoiceFactory
    {
        InvoiceHeader CreateInvoice(string[] csvRow);
    }
}