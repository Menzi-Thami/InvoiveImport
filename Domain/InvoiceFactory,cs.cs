namespace InvoiceImporter.Domain
{
    public class InvoiceFactory : IInvoiceFactory
    {
        public Invoice CreateInvoice(string[] csvRow)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = csvRow[0], // Assuming invoice number is in the first column
                InvoiceDate = DateTime.Parse(csvRow[1]), // Assuming invoice date is in the second column
                Address = csvRow[2] // Assuming address is in the third column
            };

            
            for (int i = 3; i < csvRow.Length; i += 3) // Assuming each line contains three columns: Description, Quantity, UnitPrice
            {
                var line = new InvoiceLine
                {
                    Description = csvRow[i],
                    Quantity = double.Parse(csvRow[i + 1]),
                    UnitPrice = double.Parse(csvRow[i + 2])
                };

                invoice.AddLine(line);
            }

            return invoice;
        }
    }
}
