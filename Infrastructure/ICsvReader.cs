using System.Collections.Generic;

namespace InvoiceImporter.Infrastructure
{
    public interface ICsvReader
    {
        List<string[]> ReadCsv(string filePath);
    }
}