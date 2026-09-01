using System;
using System.Collections.Generic;
using CSVFile;
using InvoiceImporter.Application;

namespace InvoiceImporter.Infrastructure
{
    public class CsvReader : ICsvReader
    {
        public List<string[]> ReadCsv(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A CSV file path is required.", nameof(filePath));
            }

            try
            {
                var result = new List<string[]>();

                var settings = new CSVSettings()
                {
                    FieldDelimiter = ',',
                    TextQualifier = '"',
                    ForceQualifiers = true
                };

                using (var cr = CSVReader.FromFile(filePath, settings))
                {
                    foreach (string[] line in cr)
                    {
                        result.Add(line);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new CsvReadException($"Error reading CSV file '{filePath}'.", ex);
            }
        }
    }
}
