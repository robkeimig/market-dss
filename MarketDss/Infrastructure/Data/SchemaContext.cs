using System.Data.Entity;
using log4net;
using MarketDss.Infrastructure.Data.Migrations;
using MarketDss.Infrastructure.Data.Schema;

namespace MarketDss.Infrastructure.Data
{
    public class SchemaContext : DbContext
    {
        private static ILog Log = LogManager.GetLogger(nameof(SchemaContext));

        public SchemaContext() : base("MarketDss")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchemaContext, MigrationConfiguration>());
            Configuration.ProxyCreationEnabled = false;
        }

        public static void Initialize()
        {
            using (var ctx = new SchemaContext())
            {
                ctx.Database.Initialize(false);
            }
        }

        private DbSet<SettingsTableRow> Settings { get; set; }

        private DbSet<SecuritiesTableRow> Securities { get; set; }

        private DbSet<SecurityDividendsTableRow> SecurityDividends { get; set; }

        private DbSet<SecurityDailyPricesTableRow> SecurityPrices { get; set; }
    }
}
