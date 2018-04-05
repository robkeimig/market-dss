using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketDss.Infrastructure.Configuration;
using MarketDss.Vendor.Nasdaq;

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

        internal async Task PullNewDividendInformationAsync()
        {
            var nasdaqScraper = new NasdaqScraper(_serviceConfiguration.NasdaqScraperConfiguration);
            var upcomingDividends = await nasdaqScraper.GetUpcomingDividendsAsync().ConfigureAwait(false);
            foreach(var upcomingDividend in upcomingDividends)
            {
                var existingDividend = await _securitiesRepository.GetDividendAsync(upcomingDividend.Symbol, upcomingDividend.ExDividendDate).ConfigureAwait(false);
                if(existingDividend != null)
                {
                    continue;
                }

                var existingSecurity = await _securitiesRepository.GetSecurityAsync(upcomingDividend.Symbol).ConfigureAwait(false);
                if(existingSecurity == null)
                {
                    existingSecurity = new Security()
                    {
                        Symbol = upcomingDividend.Symbol
                    };
                    existingSecurity.Id = await _securitiesRepository.AddSecurityAsync(existingSecurity).ConfigureAwait(false);
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

        internal Task PullNewSecurityInformationAsync()
        {
            throw new NotImplementedException();
        }

        internal Task PullNewSecurityPricesAsync()
        {
            throw new NotImplementedException();
        }

        internal async Task<IEnumerable<Security>> GetAllSecuritiesAsync()
        {
            return await _securitiesRepository.GetAllSecuritiesAsync().ConfigureAwait(false);
        }
    }
}
