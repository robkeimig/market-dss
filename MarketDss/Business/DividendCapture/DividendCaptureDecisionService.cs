using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketDss.Business.Securities;

namespace MarketDss.Business.DividendCapture
{
    /// <summary>
    /// Service providing functionality for consumers looking to query and filter securities by various top-level properties related to dividend capture concerns.
    /// Could hypothetically be consumed by a future DividendCaptureService for automated processing once the strategy is proven.
    /// </summary>
    public class DividendCaptureDecisionService
    {
        private readonly SecuritiesService _securitiesService;

        public DividendCaptureDecisionService(SecuritiesService securitiesService)
        {
            _securitiesService = securitiesService;
        }

        internal async Task<IEnumerable<Security>> GetAllSecutiesAsync()
        {
            return await _securitiesService.GetAllSecuritiesAsync().ConfigureAwait(false);
        }

        internal async Task<Security> GetSecurityAsync(int securityId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Will have the securities service pull latest dividend information, and then trigger a refresh run to update any missing securities, as well as update price info for all.
        /// </summary>
        /// <returns></returns>
        internal async Task RunBatchAsync()
        {
            await _securitiesService.PullNewDividendInformationAsync().ConfigureAwait(false);
            //await _securitiesService.PullNewSecurityInformationAsync().ConfigureAwait(false);
            //await _securitiesService.PullNewSecurityPricesAsync().ConfigureAwait(false);
        }
    }
}
