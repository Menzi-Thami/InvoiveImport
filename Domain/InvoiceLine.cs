namespace InvoiceImporter.Domain
{
    public class InvoiceLine
    {
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }

        public double CalculateLineTotal()
        {
            return Quantity * UnitPrice;
        }
    }
}
