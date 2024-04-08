namespace InvoiceImporter.Domain.Services
{
    public interface IDateTimeParser
    {
        DateTime ParseDateTime(string dateTimeString);
    }
}
