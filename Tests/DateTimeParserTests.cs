using System;
using InvoiceImporter.Domain.Services;
using Shouldly;
using Xunit;

namespace InvoiceImporter.Tests
{
    public class DateTimeParserTests
    {
        private readonly DateTimeParser _parser = new();

        [Fact]
        public void ParseDateTime_ParsesUkFormat()
        {
            var result = _parser.ParseDateTime("07/04/2024 14:30");

            result.ShouldBe(new DateTime(2024, 4, 7, 14, 30, 0));
        }

        [Fact]
        public void ParseDateTime_FallsBackToUsFormat()
        {
            // 13 cannot be a month, so only MM/dd/yyyy matches.
            var result = _parser.ParseDateTime("12/13/2024 09:05");

            result.ShouldBe(new DateTime(2024, 12, 13, 9, 5, 0));
        }

        [Theory]
        [InlineData("not-a-date")]
        [InlineData("2024-04-07")]
        [InlineData("")]
        public void ParseDateTime_ThrowsOnUnparseableInput(string value)
        {
            Should.Throw<ArgumentException>(() => _parser.ParseDateTime(value));
        }
    }
}
