using System;
using System.Collections.Generic;
using System.Linq;
using MarketDss.Infrastructure.Data.Schema;

namespace MarketDss.Business.Securities
{
    public partial class SecuritiesRepository
    {
        private Security MapToSecurity(SecuritiesTableRow row)
        {
            return new Security()
            {
                Id = row.Id,
                MarketCapitalization = row.MarketCapitalization,
                NextExDividendDate = row.NextExDividendDate,
                PriceHigh52Weeks = row.PriceHigh52Weeks,
                PriceLow52Weeks = row.PriceLow52Weeks,
                PriceToEarningsRatio = row.PriceToEarningsRatio,
                Sector = row.Sector,
                Symbol = row.Symbol
            };
        }

        private IEnumerable<Security> MapToSecurities(IEnumerable<SecuritiesTableRow> rows)
        {
            return rows.Select(securitiesTableRow => MapToSecurity(securitiesTableRow));
        }

        private SecuritiesTableRow MapToSecuritiesTableRow(Security security)
        {
            return new SecuritiesTableRow()
            {
                Id = security.Id,
                MarketCapitalization = security.MarketCapitalization,
                NextExDividendDate = security.NextExDividendDate == null ? security.NextExDividendDate.Value.Date : (DateTime?)null,
                PriceHigh52Weeks = security.PriceHigh52Weeks,
                PriceLow52Weeks = security.PriceLow52Weeks,
                PriceToEarningsRatio = security.PriceToEarningsRatio,
                Sector = security.Sector,
                Symbol = security.Symbol
            };
        }

        private SecurityDividend MapToSecurityDividend(SecurityDividendsTableRow row)
        {
            return new SecurityDividend()
            {
                Id = row.Id,
                AnnouncementDate = row.AnnouncementDate.HasValue ? row.AnnouncementDate.Value.Date : (DateTime?)null,
                Dividend = row.Dividend,
                ExDividendDate = row.ExDividendDate.HasValue ? row.ExDividendDate.Value.Date : (DateTime?)null,
                PaymentDate = row.PaymentDate.HasValue ? row.PaymentDate.Value.Date : (DateTime?)null,
                RecordDate = row.RecordDate.HasValue ? row.RecordDate.Value.Date : (DateTime?)null,
                SecurityId = row.SecurityId
            };
        }
    }
}
