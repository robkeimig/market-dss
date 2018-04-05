using MarketDss.Vendor.Nasdaq;
using Newtonsoft.Json;

namespace MarketDss.Infrastructure.Configuration
{
    public class ServiceConfiguration
    {
        public string UrlBinding { get; internal set; }

        public bool IsDebuggerAttached { get; internal set; }

        public string ConnectionString { get; internal set; }

        public string ServicePath { get; internal set; }

        public string WwwRootParentPath { get; internal set; }

        public JsonSerializerSettings JsonSerializerSettings { get; internal set; }

        public NasdaqScraperConfiguration NasdaqScraperConfiguration { get; internal set; }
    }
}
