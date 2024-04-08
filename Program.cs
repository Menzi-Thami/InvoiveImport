using InvoiceImporter.Application;
using InvoiceImporter.Domain;
using InvoiceImporter.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using InvoiceImporter.Domain.Services; // Add this namespace

namespace InvoiceImporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Prompt user for CSV file path
                Console.Write("Enter the file path of the CSV file: ");
                string filePath = Console.ReadLine();

                // Replace special characters in the file path
                filePath = filePath.Replace("\"", "").Replace("\\", "\\\\");

                // Read CSV data
                var csvReader = new CsvReader();
                var csvData = csvReader.ReadCsv(filePath);

                // Configure DbContext and Repository
                using (var dbContext = new InvoiceDbContext())
                {
                    var repository = new InvoiceRepository(dbContext);
                    var logger = new ConsoleLogger();
                    var dateTimeParser = new DateTimeParser(); // Instantiate DateTimeParser
                    var invoiceFactory = new InvoiceFactory(dateTimeParser); // Pass DateTimeParser to InvoiceFactory
                    var dataImporter = new DataImporter(csvReader, logger, repository, invoiceFactory);

                    // Import data
                    await dataImporter.ImportData(filePath);
                }
            }
            catch (Exception ex)
            {
                // Handle and display any exceptions
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
