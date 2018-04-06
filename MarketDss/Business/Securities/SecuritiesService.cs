using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketDss.Infrastructure.Configuration;
using MarketDss.Vendor.Nasdaq;
using MarketDss.Vendor.Robinhood;

namespace MarketDss.Business.Securities
{
    public class SecuritiesService
    {
        private readonly ServiceConfiguration _serviceConfiguration;
        private readonly SecuritiesRepository _securitiesRepository;

        public SecuritiesService(
            ServiceConfiguration serviceConfiguration,
            SecuritiesRepository securitiesRepository)
        {
            _serviceConfiguration = serviceConfiguration;
            _securitiesRepository = securitiesRepository;
        }

        public async Task<IEnumerable<Security>> GetAllSecuritiesAsync()
        {
            return await _securitiesRepository.GetAllSecuritiesAsync().ConfigureAwait(false);
        }

        public async Task RefreshAsync()
        {
            await PullNewDividendInformationAsync().ConfigureAwait(false);
            await PullNewSecurityInformationAsync().ConfigureAwait(false);
        }

        private async Task PullNewDividendInformationAsync()
        {
            var nasdaqScraper = new NasdaqScraper(_serviceConfiguration.NasdaqScraperConfiguration);
            var upcomingDividends = await nasdaqScraper.GetUpcomingDividendsAsync().ConfigureAwait(false);
            foreach(var upcomingDividend in upcomingDividends)
            {
                var existingSecurity = await _securitiesRepository.GetSecurityAsync(upcomingDividend.Symbol).ConfigureAwait(false);
                if (existingSecurity == null)
                {
                    existingSecurity = new Security()
                    {
                        Symbol = upcomingDividend.Symbol,
                        Dividends = new List<SecurityDividend>(),
                        DailyPriceHistory = new List<SecurityDailyPriceHistory>()
                    };
                    existingSecurity.Id = await _securitiesRepository.AddSecurityAsync(existingSecurity).ConfigureAwait(false);
                }

                var existingDividend = existingSecurity.Dividends.FirstOrDefault(d => d.ExDividendDate.Value.Date == upcomingDividend.ExDividendDate.Value.Date);
                if(existingDividend != null)
                {
                    continue;
                }

                existingDividend = new SecurityDividend()
                {
                    Symbol = existingSecurity.Symbol,
                    SecurityId = existingSecurity.Id,
                    AnnouncementDate = upcomingDividend.AnnouncementDate,
                    ExDividendDate = upcomingDividend.ExDividendDate,
                    PaymentDate = upcomingDividend.PaymentDate,
                    RecordDate = upcomingDividend.RecordDate,
                    Dividend = upcomingDividend.Dividend
                };
                existingDividend.Id = await _securitiesRepository.AddSecurityDividendAsync(existingDividend).ConfigureAwait(false);
            }
        }

        private async Task PullNewSecurityInformationAsync()
        {
            var securities = await _securitiesRepository.GetAllSecuritiesAsync().ConfigureAwait(false);
            var robinhoodClient = new RobinhoodClient(_serviceConfiguration.RobinhoodClientConfiguration);
            foreach(var security in securities)
            {
                var fundamentals = await robinhoodClient.GetFundamentalsAsync(security.Symbol).ConfigureAwait(false);
                var priceHistory = await robinhoodClient.GetPriceHistoryAsync(security.Symbol).ConfigureAwait(false);
                // TODO : security.MarketCapitalization = fundamentals.xyz...
                // TODO : security price mappings and table update
                await _securitiesRepository.UpdateSecurityAsync(security).ConfigureAwait(false);
            }
        }
    }
}
