using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDss.Infrastructure.Configuration
{
    public partial class SettingRepository
    {
        private readonly string _connectionString;

        public SettingRepository(ServiceConfiguration configuration)
        {
            _connectionString = configuration.ConnectionString;
        }

        public SettingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal IEnumerable<Setting> GetAllSettings()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var result = con.Query<Setting>(SelectAllQuery);
                return result;
            }
        }

        internal void RemoveSetting(string name)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Execute(DeleteByNameQuery, new { Name = name });
            }
        }

        internal void AddSetting(string name, string value)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Execute(InsertQuery, new { Name = name, Value = value });
            }
        }

        internal string GetSettingValue(string name)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var result = con.QueryFirst<Setting>(SelectByNameQuery, new { Name = name });
                return result.Value;
            }
        }

        internal async Task UpdateSettingAsync(Setting setting)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(UpdateByNameQuery, setting).ConfigureAwait(false);
            }
        }

        internal void UpdateSetting(Setting setting)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Execute(UpdateByNameQuery, setting);
            }
        }

        internal async Task<IEnumerable<Setting>> GetAllSettingsAsync()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<Setting>(SelectAllQuery).ConfigureAwait(false);
                return result;
            }
        }
    }
}
