using Flurl.Http;
using MarketDss.Vendor.Robinhood.Models;
using System;
using System.Threading.Tasks;

namespace MarketDss.Vendor.Robinhood
{
    public class RobinhoodClient
    {
        private readonly RobinhoodClientConfiguration _configuration;

        public RobinhoodClient(RobinhoodClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Fundamental> GetFundamentalsAsync(string symbol)
        {
            var baseUri = new Uri(_configuration.Url);
            var url = new Uri(baseUri, $"fundamentals/{symbol}/").ToString();
            var fundamental = await url.GetJsonAsync<Fundamental>().ConfigureAwait(false);
            return fundamental;
        }

        public async Task<RobinhoodPriceHistory> GetPriceHistoryAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
