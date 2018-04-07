using Newtonsoft.Json;

namespace MarketDss.Vendor.Robinhood.Models
{
    public class Fundamental
    {
        [JsonProperty("open")]
        public decimal? Open { get; set; }

        [JsonProperty("high")]
        public decimal? High { get; set; }

        [JsonProperty("low")]
        public decimal? Low { get; set; }

        [JsonProperty("average_volume_2_weeks")]
        public decimal? AverageVolume2Weeks { get; set; }

        [JsonProperty("average_volume")]
        public decimal? AverageVolume { get; set; }

        [JsonProperty("high_52_weeks")]
        public decimal? High52Weeks { get; set; }

        [JsonProperty("dividend_yield")]
        public decimal? DividendYield { get; set; }

        [JsonProperty("low_52_weeks")]
        public decimal? Low52Weeks { get; set; }

        [JsonProperty("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonProperty("pe_ratio")]
        public decimal? PriceToEarningsRatio { get; set; }

        [JsonProperty("shares_outstanding")]
        public decimal? SharesOutstanding { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("instrument")]
        public string Instrument { get; set; }

        [JsonProperty("ceo")]
        public string Ceo { get; set; }

        [JsonProperty("headquarters_city")]
        public string HeadquartersCity { get; set; }

        [JsonProperty("headquarters_state")]
        public string HeadquartersState { get; set; }

        [JsonProperty("sector")]
        public string Sector { get; set; }

        [JsonProperty("num_employees")]
        public int? Employees { get; set; }

        [JsonProperty("year_founded")]
        public int? YearFounded { get; set; }
    }
}
