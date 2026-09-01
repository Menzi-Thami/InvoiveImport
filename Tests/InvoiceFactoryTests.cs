using System;
using InvoiceImporter.Domain;
using InvoiceImporter.Domain.Services;
using Shouldly;
using Xunit;

namespace InvoiceImporter.Tests
{
    public class InvoiceFactoryTests
    {
        private static readonly DateTime FixedDate = new(2024, 4, 7, 14, 30, 0);

        private readonly InvoiceFactory _factory = new(new StubDateTimeParser(FixedDate));

        [Fact]
        public void CreateInvoice_MapsHeaderFields()
        {
            var row = new[] { "INV-001", "07/04/2024 14:30", "1 High St", "120.50", "Widget", "2", "60.25" };

            var invoice = _factory.CreateInvoice(row);

            invoice.InvoiceNumber.ShouldBe("INV-001");
            invoice.InvoiceDate.ShouldBe(FixedDate);
            invoice.Address.ShouldBe("1 High St");
            invoice.InvoiceTotal.ShouldBe(120.50);
        }

        [Fact]
        public void CreateInvoice_MapsSingleLine()
        {
            var row = new[] { "INV-001", "07/04/2024 14:30", "1 High St", "120.50", "Widget", "2", "60.25" };

            var invoice = _factory.CreateInvoice(row);

            invoice.Lines.Count.ShouldBe(1);
            var line = invoice.Lines[0];
            line.Description.ShouldBe("Widget");
            line.Quantity.ShouldBe(2);
            line.UnitSellingPriceExVAT.ShouldBe(60.25);
        }

        [Fact]
        public void CreateInvoice_MapsMultipleLines()
        {
            var row = new[]
            {
                "INV-002", "07/04/2024 14:30", "2 Low St", "300",
                "Widget", "2", "60.25",
                "Gadget", "1", "40"
            };

            var invoice = _factory.CreateInvoice(row);

            invoice.Lines.Count.ShouldBe(2);
            invoice.Lines[1].Description.ShouldBe("Gadget");
            invoice.Lines[1].Quantity.ShouldBe(1);
        }

        [Fact]
        public void CreateInvoice_IgnoresTrailingPartialLine()
        {
            // Two extra columns is not a full 3-column line and must not throw.
            var row = new[]
            {
                "INV-003", "07/04/2024 14:30", "3 Mid St", "60",
                "Widget", "2", "60.25",
                "Gadget", "1" // incomplete
            };

            var invoice = _factory.CreateInvoice(row);

            invoice.Lines.Count.ShouldBe(1);
        }

        [Fact]
        public void CreateInvoice_TreatsNonNumericAmountsAsZero()
        {
            var row = new[] { "INV-004", "07/04/2024 14:30", "4 Top St", "n/a", "Widget", "-", "abc" };

            var invoice = _factory.CreateInvoice(row);

            invoice.InvoiceTotal.ShouldBe(0);
            invoice.Lines[0].Quantity.ShouldBe(0);
            invoice.Lines[0].UnitSellingPriceExVAT.ShouldBe(0);
        }

        [Fact]
        public void CreateInvoice_ThrowsWhenRowIsNull()
        {
            Should.Throw<ArgumentException>(() => _factory.CreateInvoice(null!));
        }

        [Fact]
        public void CreateInvoice_ThrowsWhenTooFewColumns()
        {
            var row = new[] { "INV-005", "07/04/2024 14:30", "5 Any St", "10" };

            Should.Throw<ArgumentException>(() => _factory.CreateInvoice(row));
        }

        [Fact]
        public void CreateInvoice_ThrowsWhenInvoiceNumberIsBlank()
        {
            var row = new[] { "  ", "07/04/2024 14:30", "6 Any St", "10", "Widget", "1", "10" };

            Should.Throw<ArgumentException>(() => _factory.CreateInvoice(row));
        }

        private sealed class StubDateTimeParser : IDateTimeParser
        {
            private readonly DateTime _value;

            public StubDateTimeParser(DateTime value) => _value = value;

            public DateTime ParseDateTime(string dateTimeString) => _value;
        }
    }
}
