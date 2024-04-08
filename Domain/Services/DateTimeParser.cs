using System;

namespace InvoiceImporter.Domain.Services
{
    public class DateTimeParser : IDateTimeParser
    {
        public DateTime ParseDateTime(string dateTimeString)
        {
            // Define possible date formats
            var dateFormats = new string[] { "dd/MM/yyyy HH:mm", "MM/dd/yyyy HH:mm" };

            DateTime invoiceDate = DateTime.MinValue; // Initialize with a default value
            bool dateParsed = false;

            foreach (var format in dateFormats)
            {
                if (DateTime.TryParseExact(dateTimeString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out invoiceDate))
                {
                    dateParsed = true;
                    break;
                }
            }

            if (!dateParsed)
            {
                throw new ArgumentException($"Unable to parse the date string: {dateTimeString}");
            }

            return invoiceDate;
        }
    }
}
