using System;

namespace MarketDss.Business.Securities
{
    public class SecurityDividend
    {
        public int Id { get; set; }

        public int? SecurityId { get; set; }

        public string Symbol { get; set; }

        public double? Dividend { get; set; }

        public DateTime? ExDividendDate { get; set; }

        public DateTime? RecordDate { get; set; }

        public DateTime? AnnouncementDate { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
