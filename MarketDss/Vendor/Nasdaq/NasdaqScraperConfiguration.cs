namespace MarketDss.Vendor.Nasdaq
{
    public class NasdaqScraperConfiguration
    {
        public string Url => "https://www.nas"+"daq.com/dividend-stocks/dividend-calendar.aspx";

        public int LookupDays { get; set; }

        public int RequestDelaySeconds { get; set; }
    }
}
