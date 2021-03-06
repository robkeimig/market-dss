﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketDss.Infrastructure.Data.Schema
{
    [Table("Securities")]
    public class SecuritiesTableRow
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
    }
}
