using System.Data.Entity.Migrations;

namespace MarketDss.Infrastructure.Data.Migrations
{
    internal sealed class MigrationConfiguration : DbMigrationsConfiguration<SchemaContext>
    {
        public MigrationConfiguration()
        {
            MigrationsDirectory = @"Infrastructure\Data\Migrations";
            ContextKey = "MarketDss";
            AutomaticMigrationsEnabled = false;
        }
    }
}
