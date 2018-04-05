using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketDss.Infrastructure.Data.Schema
{
    [Table("SecurityDividends")]
    public class SecurityDividendsTableRow
    {
        public int Id { get; set; }

        public int SecurityId { get; set; }

        public double Dividend { get; set; }

        public DateTime ExDividendDate { get; set; }

        public DateTime RecordDate { get; set; }

        public DateTime AnnouncementDate { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
