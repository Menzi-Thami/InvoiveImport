using System.Collections.Generic;

namespace InvoiceImporter.Application
{
    /// <summary>
    /// Port for reading rows out of a CSV source. Owned by the Application layer
    /// (the consumer) so the flow of dependencies points inward; the concrete
    /// CSV-library implementation lives in Infrastructure.
    /// </summary>
    public interface ICsvReader
    {
        List<string[]> ReadCsv(string filePath);
    }
}
