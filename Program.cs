using System;
using System.Threading.Tasks;
using InvoiceImporter.Application;
using InvoiceImporter.Domain;
using InvoiceImporter.Domain.Services;
using InvoiceImporter.Infrastructure;

namespace InvoiceImporter
{
    /// <summary>
    /// Composition root: reads the file path, wires the dependencies, and runs the import.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.Write("Enter the file path of the CSV file: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentException("No file path was entered.");
                }

                // Strip quotes added by "Copy as path" and normalise separators.
                string filePath = input.Replace("\"", "").Replace("\\", "\\\\");

                using var dbContext = new InvoiceDbContext();

                var csvReader = new CsvReader();
                var logger = new ConsoleLogger();
                var repository = new InvoiceRepository(dbContext);
                var dateTimeParser = new DateTimeParser();
                var invoiceFactory = new InvoiceFactory(dateTimeParser);
                var dataImporter = new DataImporter(csvReader, logger, repository, invoiceFactory);

                await dataImporter.ImportData(filePath);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
