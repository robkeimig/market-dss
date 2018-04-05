using System;

namespace MarketDss.Vendor.Nasdaq
{
    public class NasdaqDividendItem
    {
        public DateTime FetchedDateUtc { get; set; }

        public string Symbol { get; set; }

        public double? Dividend { get; set; }

        public DateTime? ExDividendDate { get; set; }

        public DateTime? RecordDate { get; set; }

        public DateTime? AnnouncementDate { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
