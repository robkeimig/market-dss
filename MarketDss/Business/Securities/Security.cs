using System;

namespace MarketDss.Business.Securities
{
    public class Security
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Sector { get; set; }

        public decimal PriceToEarningsRatio { get; set; }

        public decimal PriceLow52Weeks { get; set; }

        public decimal PriceHigh52Weeks { get; set; }

        public decimal MarketCapitalization { get; set; }

        public DateTime? NextExDividendDate { get; set; }
    }
}
