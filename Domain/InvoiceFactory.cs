using System;
using System.Globalization;
using InvoiceImporter.Domain.Services;

namespace InvoiceImporter.Domain
{
    public class InvoiceFactory : IInvoiceFactory
    {
        private const int MinimumColumns = 7;
        private const int FirstLineColumn = 4;
        private const int ColumnsPerLine = 3;

        private readonly IDateTimeParser _dateTimeParser;

        public InvoiceFactory(IDateTimeParser dateTimeParser)
        {
            _dateTimeParser = dateTimeParser;
        }

        public InvoiceHeader CreateInvoice(string[] csvRow)
        {
            if (csvRow == null || csvRow.Length < MinimumColumns)
            {
                throw new ArgumentException(
                    $"Invalid CSV row format: expected at least {MinimumColumns} columns.",
                    nameof(csvRow));
            }

            var invoiceDate = _dateTimeParser.ParseDateTime(csvRow[1]);

            var invoice = new InvoiceHeader(
                invoiceNumber: csvRow[0],
                invoiceDate: invoiceDate,
                address: csvRow[2],
                invoiceTotal: ParseDouble(csvRow[3]));

            // Each line occupies three columns: Description, Quantity, UnitPrice.
            for (int i = FirstLineColumn; i + ColumnsPerLine - 1 < csvRow.Length; i += ColumnsPerLine)
            {
                invoice.AddLine(new InvoiceLine(
                    description: csvRow[i],
                    quantity: ParseDouble(csvRow[i + 1]),
                    unitSellingPriceExVat: ParseDouble(csvRow[i + 2])));
            }

            return invoice;
        }

        private static double ParseDouble(string value)
        {
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            return 0;
        }
    }
}
