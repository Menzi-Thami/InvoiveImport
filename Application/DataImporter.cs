using Microsoft.Extensions.Logging;
using InvoiceImporter.Domain;

namespace InvoiceImporter.Application
{
    public class DataImporter : IDataImporter
    {
        private readonly ICsvReader _csvReader;
        private readonly ILogger<DataImporter> _logger;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceFactory _invoiceFactory;

        public DataImporter(
            ICsvReader csvReader,
            ILogger<DataImporter> logger,
            IInvoiceRepository invoiceRepository,
            IInvoiceFactory invoiceFactory)
        {
            _csvReader = csvReader;
            _logger = logger;
            _invoiceRepository = invoiceRepository;
            _invoiceFactory = invoiceFactory;
        }

        public Task ImportData(string filePath)
        {
            _logger.LogInformation("Reading CSV file {FilePath}", filePath);

            List<string[]> csvData = _csvReader.ReadCsv(filePath);

            int imported = 0, skipped = 0;
            foreach (var row in csvData.Skip(1)) // Skip header row
            {
                var invoiceNumber = row[0];
                if (_invoiceRepository.InvoiceExists(invoiceNumber))
                {
                    _logger.LogWarning("Invoice {InvoiceNumber} already exists; skipping", invoiceNumber);
                    skipped++;
                    continue;
                }

                var invoice = _invoiceFactory.CreateInvoice(row);
                _invoiceRepository.AddInvoice(invoice);
                imported++;
                _logger.LogDebug("Invoice {InvoiceNumber} queued for import", invoiceNumber);
            }

            _invoiceRepository.SaveChanges();

            _logger.LogInformation(
                "Import completed: {Imported} imported, {Skipped} skipped from {FilePath}",
                imported, skipped, filePath);

            return Task.CompletedTask;
        }
    }
}
