using InvoiceImporter.Domain;
using InvoiceImporter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceImporter.Application
{
    public class DataImporter : IDataImporter
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceFactory _invoiceFactory;
        private readonly ILogger _logger;

        public DataImporter(IInvoiceRepository invoiceRepository, IInvoiceFactory invoiceFactory, ILogger logger)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceFactory = invoiceFactory;
            _logger = logger;
        }

        public void ImportData(List<string[]> csvData)
        {
            try
            {
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

                    _logger.Log($"Invoice {invoiceNumber} imported. Total Amount: {invoice.CalculateTotalAmount()}");
                }

                _invoiceRepository.SaveChanges();

                // Check data integrity
                CheckDataIntegrity(csvData);
            }
            catch (Exception ex)
            {
                _logger.Log($"An error occurred: {ex.Message}");
            }
        }

        private void CheckDataIntegrity(List<string[]> csvData)
        {
            var expectedTotal = csvData.Skip(1).Sum(row => double.Parse(row[5]) * double.Parse(row[6]));
            var actualTotal = _invoiceRepository.GetInvoiceLinesTotal();

            if (Math.Abs(expectedTotal - actualTotal) < 0.01)
            {
                _logger.Log($"Data integrity check passed. Expected total: {expectedTotal}, Actual total: {actualTotal}");
            }
            else
            {
                _logger.Log($"Data integrity check failed. Expected total: {expectedTotal}, Actual total: {actualTotal}");
            }
        }
    }
}
