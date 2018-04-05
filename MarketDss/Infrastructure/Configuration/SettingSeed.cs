using System.Collections.Generic;

namespace MarketDss.Infrastructure.Configuration
{
    public static class SettingSeed
    {
        private const string DefaultUriBinding = @"http://localhost:8080";
        private const int DefaultNasdaqScraperLookupDays = 7;
        private const int DefaultRequestDelaySeconds = 1;

        public static IEnumerable<Setting> Settings => new List<Setting>()
        {
            new Setting() { Name = @"UrlBinding", Value = DefaultUriBinding },
            new Setting() { Name = @"NasdaqScraperLookupDays", Value = DefaultNasdaqScraperLookupDays.ToString() },
            new Setting() { Name = @"RequestDelaySeconds", Value = DefaultRequestDelaySeconds.ToString() },
        };
    }
}
