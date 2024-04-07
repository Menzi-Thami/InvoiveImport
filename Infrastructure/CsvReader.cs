using CSVFile;
using System.Collections.Generic;

namespace InvoiceImporter.Infrastructure
{
    public class CsvReader : ICsvReader
    {
        public List<string[]> ReadCsv(string filePath)
        {
            var result = new List<string[]>();

            // Custom CSV settings
            var settings = new CSVSettings()
            {
                FieldDelimiter = ',',
                TextQualifier = '"',
                ForceQualifiers = true
            };

            // Use asynchronous I/O to stream CSV data off disk
            using (var cr = CSVReader.FromFile(filePath, settings))
            {
                foreach (string[] line in cr)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
