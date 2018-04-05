using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        internal Task<SecurityDividend> GetDividendAsync(string symbol, DateTime? exDividendDate)
        {
            throw new NotImplementedException();
        }

        internal Task<int> AddSecurityAsync(Security security)
        {
            throw new NotImplementedException();
        }

        internal Task<Security> GetSecurityAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        internal Task<int> AddSecurityDividendAsync(SecurityDividend securityDividend)
        {
            throw new NotImplementedException();
        }

        internal Task UpdateSecurityAsync(Security security)
        {
            throw new NotImplementedException();
        }
    }
}