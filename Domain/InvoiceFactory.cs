using System;
using System.Collections.Generic;
using InvoiceImporter.Domain.Services;

namespace InvoiceImporter.Domain
{
    public class InvoiceFactory : IInvoiceFactory
    {
        private readonly IDateTimeParser _dateTimeParser;

        public InvoiceFactory(IDateTimeParser dateTimeParser)
        {
            _dateTimeParser = dateTimeParser;
        }

        public InvoiceHeader CreateInvoice(string[] csvRow)
        {
            if (csvRow == null || csvRow.Length < 7) // Check if the row has at least 7 columns
            {
                throw new ArgumentException("Invalid CSV row format");
            }

            var invoiceDate = _dateTimeParser.ParseDateTime(csvRow[1]); // Parse invoice date using injected service

            var invoice = new InvoiceHeader
            {
                InvoiceNumber = csvRow[0], 
                InvoiceDate = invoiceDate,
                Address = csvRow[2], 
                InvoiceTotal = ParseDouble(csvRow[3]), 
                Lines = new List<InvoiceLine>()
            };

           //interate three columns: Description, Quantity, UnitPrice, interation 
            for (int i = 4; i < csvRow.Length; i += 3)
            {
                var line = new InvoiceLine
                {
                    Description = csvRow[i],
                    Quantity = ParseDouble(csvRow[i + 1]),
                    UnitSellingPriceExVAT = ParseDouble(csvRow[i + 2])
                };

                invoice.Lines.Add(line);
            }

            return invoice;
        }

        private double ParseDouble(string value)
        {
            if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            return 0; // Or throw exception, depending on your error handling strategy
        }
    }
}
