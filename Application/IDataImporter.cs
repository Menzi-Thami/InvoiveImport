using System.Collections.Generic;

namespace InvoiceImporter.Application
{
    public interface IDataImporter
    {
        void ImportData(List<string[]> csvData);
    }
}
