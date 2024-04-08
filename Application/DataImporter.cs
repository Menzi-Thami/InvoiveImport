using InvoiceImporter.Domain;
using InvoiceImporter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceImporter.Application
{
    public class DataImporter : IDataImporter
    {
        private readonly ICsvReader _csvReader;
        private readonly ILogger _logger;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceFactory _invoiceFactory;

        public DataImporter(ICsvReader csvReader, ILogger logger, IInvoiceRepository invoiceRepository, IInvoiceFactory invoiceFactory)
        {
            _csvReader = csvReader;
            _logger = logger;
            _invoiceRepository = invoiceRepository;
            _invoiceFactory = invoiceFactory;
        }

        public async Task ImportData(string filePath)
        {
            try
            {
                _logger.Log("Reading CSV file...");

                List<string[]> csvData = _csvReader.ReadCsv(filePath);

                _logger.Log("Importing data from CSV...");

                foreach (var row in csvData.Skip(1)) // Skip header row
                {
                    var invoiceNumber = row[0];
                    if (_invoiceRepository.InvoiceExists(invoiceNumber))
                    {
                        _logger.Log($"Invoice {invoiceNumber} already exists. Skipping...");
                        continue;
                    }

                    var invoice = _invoiceFactory.CreateInvoice(row);
                    _invoiceRepository.AddInvoice(invoice);

                    _logger.Log($"Invoice {invoiceNumber} imported.");
                }

                _invoiceRepository.SaveChanges();

                _logger.Log("Data import completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.Log($"An error occurred: {ex.Message}");
            }
        }
    }
}
