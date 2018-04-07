using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MarketDss.Infrastructure.Configuration;
using MarketDss.Infrastructure.Data.Schema;

namespace MarketDss.Business.Securities
{
    public partial class SecuritiesRepository
    {
        private readonly ServiceConfiguration _serviceConfiguration;

        public SecuritiesRepository(ServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public async Task<IEnumerable<Security>> GetAllSecuritiesAsync()
        {
            using(var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                var securitiesTableRows = await con.QueryAsync<SecuritiesTableRow>(SelectAllSecuritiesQuery).ConfigureAwait(false);
                var securities = MapToSecurities(securitiesTableRows);
                return securities;
            }
        }

        internal async Task<int> AddSecurityAsync(Security security)
        {
            using(var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                var securitiesTableRow = MapToSecuritiesTableRow(security);

                var id = await con
                    .ExecuteScalarAsync<int>(InsertSecurityQuery, securitiesTableRow)
                    .ConfigureAwait(false);

                return id;
            }
        }

        internal async Task<Security> GetSecurityAsync(string symbol)
        {
            using (var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                var securitiesTableRow = await con
                    .QueryFirstOrDefaultAsync<SecuritiesTableRow>(SelectSecurityBySymbolQuery, new { Symbol = symbol })
                    .ConfigureAwait(false);

                if(securitiesTableRow == null)
                {
                    return null;
                }

                var security = MapToSecurity(securitiesTableRow);
                var securityDividendsTableRows = await con
                    .QueryAsync<SecurityDividendsTableRow>(SelectSecurityDividendBySecurityIdQuery, new { SecurityId = security.Id })
                    .ConfigureAwait(false);

                if(!securityDividendsTableRows.Any())
                {
                    security.Dividends = new List<SecurityDividend>();
                }
                else
                {
                    security.Dividends = MapToSecurityDividends(securityDividendsTableRows);
                }

                return security;
            }
        }

        internal async Task<int> AddSecurityDividendAsync(SecurityDividend securityDividend)
        {
            using(var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                var securityDividendsTableRow = MapToSecurityDividendsTableRow(securityDividend);

                var id = await con
                    .ExecuteScalarAsync<int>(InsertSecurityDividendQuery, securityDividendsTableRow)
                    .ConfigureAwait(false);

                return id;
            }
        }

        internal async Task UpdateSecurityDividendAsync(SecurityDividend securityDividend)
        {
            using (var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                var securityDividendsTableRow = MapToSecurityDividendsTableRow(securityDividend);
                await con.ExecuteAsync(UpdateSecurityDividendByIdQuery, securityDividendsTableRow).ConfigureAwait(false);
            }
        }

        internal async Task UpdateSecurityAsync(Security security)
        {
            using(var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                foreach (var dividend in security.Dividends)
                {
                    await UpdateSecurityDividendAsync(dividend).ConfigureAwait(false);
                }
                var securitiesTableRow = MapToSecuritiesTableRow(security);
                await con.ExecuteAsync(UpdateSecurityByIdQuery, securitiesTableRow).ConfigureAwait(false);
            }
        }

        internal async Task<SecurityDividend> GetNextSecurityDividendAsync(int securityId)
        {
            using(var con = new SqlConnection(_serviceConfiguration.ConnectionString))
            {
                var securityDividendsTableRows = await con
                   .QueryAsync<SecurityDividendsTableRow>(SelectSecurityDividendBySecurityIdQuery, new { SecurityId = securityId })
                   .ConfigureAwait(false);

                if (!securityDividendsTableRows.Any())
                {
                    return null;
                }
                else
                {
                    var securityDividends = MapToSecurityDividends(securityDividendsTableRows);
                    var securityDividend = securityDividends.Where(x => x.ExDividendDate.HasValue && x.ExDividendDate >= DateTime.Today).OrderBy(y => y.ExDividendDate.Value).FirstOrDefault();
                    return securityDividend;
                }
            }
        }
    }
}