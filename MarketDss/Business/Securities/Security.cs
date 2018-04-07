using System;
using System.Collections.Generic;

namespace MarketDss.Business.Securities
{
    public class Security
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Sector { get; set; }

        public decimal? PriceToEarningsRatio { get; set; }

        public decimal? PriceLow52Weeks { get; set; }

        public decimal? PriceHigh52Weeks { get; set; }

        public decimal? MarketCapitalization { get; set; }

        public DateTime? NextExDividendDate { get; set; }

        public decimal? NextDividendAmount { get; set; }

        public decimal? ComputedRelativeStrengthIndex { get; set; }

        public decimal? ComputedDividendYield { get; set; }

        public IEnumerable<SecurityDividend> Dividends { get; set; } = new List<SecurityDividend>();

        public IEnumerable<SecurityDailyPriceHistory> DailyPriceHistory { get; set; } = new List<SecurityDailyPriceHistory>();
    }
}
