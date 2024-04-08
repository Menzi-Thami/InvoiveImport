using CSVFile;
using System;
using System.Collections.Generic;

namespace InvoiceImporter.Infrastructure
{
    public class CsvReader : ICsvReader
    {
        public List<string[]> ReadCsv(string filePath)
        {
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
                throw new Exception($"Error reading CSV file: {ex.Message}");
            }
        }
    }
}