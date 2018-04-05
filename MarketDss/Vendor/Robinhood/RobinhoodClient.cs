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

        public async Task<RobinhoodFundamentals> GetFundamentalsAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<RobinhoodPriceHistory> GetPriceHistoryAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
