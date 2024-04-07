using InvoiceImporter.Application;
using System;

namespace InvoiceImporter.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
