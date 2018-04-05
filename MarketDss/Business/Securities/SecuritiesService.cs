﻿using System;
using System.Collections.Generic;
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

        internal async Task PullNewSecurityInformationAsync()
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

        internal async Task<IEnumerable<Security>> GetAllSecuritiesAsync()
        {
            return await _securitiesRepository.GetAllSecuritiesAsync().ConfigureAwait(false);
        }
    }
}
