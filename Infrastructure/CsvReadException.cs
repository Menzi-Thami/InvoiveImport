using System;

namespace InvoiceImporter.Infrastructure
{
    /// <summary>
    /// Thrown when a CSV file cannot be read. Preserves the underlying cause
    /// as <see cref="Exception.InnerException"/> instead of collapsing it into a
    /// bare <see cref="Exception"/> message.
    /// </summary>
    public class CsvReadException : Exception
    {
        public CsvReadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
