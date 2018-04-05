using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketDss.Infrastructure.Data.Schema
{
    [Table("SecurityDailyPrices")]
    public class SecurityDailyPricesTableRow
    {
        public int Id { get; set; }

        public int SecurityId { get; set; }

        public DateTime Date { get; set; }

        public double Open { get; set; }

        public double Close { get; set; }

        public double Low { get; set; }

        public double High { get; set; }

        public double Volume { get; set; }
    }
}
