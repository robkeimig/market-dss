using System.Collections.Generic;
using System.Linq;
using MarketDss.Infrastructure.Data.Schema;

namespace MarketDss.Business.Securities
{
    public partial class SecuritiesRepository
    {
        private Security MapToSecurity(SecuritiesTableRow securitiesTableRow)
        {
            return new Security()
            {
                Id = securitiesTableRow.Id,
                MarketCapitalization = securitiesTableRow.MarketCapitalization,
                NextExDividendDate = securitiesTableRow.NextExDividendDate,
                PriceHigh52Weeks = securitiesTableRow.PriceHigh52Weeks,
                PriceLow52Weeks = securitiesTableRow.PriceLow52Weeks,
                PriceToEarningsRatio = securitiesTableRow.PriceToEarningsRatio,
                Sector = securitiesTableRow.Sector,
                Symbol = securitiesTableRow.Symbol
            };
        }

        private IEnumerable<Security> MapToSecurities(IEnumerable<SecuritiesTableRow> securitiesTableRows)
        {
            return securitiesTableRows.Select(securitiesTableRow => MapToSecurity(securitiesTableRow));
        }

        private SecuritiesTableRow MapToSecuritiesTableRow(Security security)
        {
            return new SecuritiesTableRow()
            {
                Id = security.Id,
                MarketCapitalization = security.MarketCapitalization,
                NextExDividendDate = security.NextExDividendDate,
                PriceHigh52Weeks = security.PriceHigh52Weeks,
                PriceLow52Weeks = security.PriceLow52Weeks,
                PriceToEarningsRatio = security.PriceToEarningsRatio,
                Sector = security.Sector,
                Symbol = security.Symbol
            };
        }
    }
}
