using System.Diagnostics;
using System.IO;
using MarketDss.Vendor.Nasdaq;
using MarketDss.Vendor.Robinhood;
using Newtonsoft.Json.Serialization;

namespace MarketDss.Infrastructure.Configuration
{
    public class ConfigurationService
    {
        private readonly SettingService _settingService;
        private readonly string _connectionString;

        public ConfigurationService(SettingService settingService, string connectionString)
        {
            _settingService = settingService;
            _connectionString = connectionString;
        }

        public ServiceConfiguration GetConfiguration()
        {
            var configuration = new ServiceConfiguration
            {
                ConnectionString = _connectionString,
                UrlBinding = _settingService.GetSettingValue("UrlBinding"),
                ServicePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName),
                JsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Newtonsoft.Json.Formatting.Indented
                }
            };

            if (Debugger.IsAttached)
            {
                configuration.IsDebuggerAttached = true;
                configuration.WwwRootParentPath = $@"{Directory.GetCurrentDirectory()}\..\..\";
            }
            else
            {
                configuration.IsDebuggerAttached = false;
                configuration.WwwRootParentPath = configuration.ServicePath;
            }

            configuration.NasdaqScraperConfiguration = new NasdaqScraperConfiguration()
            {
                LookupDays = int.Parse(_settingService.GetSettingValue("NasdaqScraperLookupDays")),
                RequestDelaySeconds = int.Parse(_settingService.GetSettingValue("RequestDelaySeconds"))
            };

            configuration.RobinhoodClientConfiguration = new RobinhoodClientConfiguration()
            {
            };

            return configuration;
        }
    }
}
