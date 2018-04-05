namespace MarketDss.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecuritiesTableColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "NextDividendAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Securities", "ComputedRelativeStrengthIndex", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Securities", "ComputedDividendYield", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "PriceToEarningsRatio", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "PriceLow52Weeks", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "PriceHigh52Weeks", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "MarketCapitalization", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Securities", "MarketCapitalization", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "PriceHigh52Weeks", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "PriceLow52Weeks", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Securities", "PriceToEarningsRatio", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Securities", "ComputedDividendYield");
            DropColumn("dbo.Securities", "ComputedRelativeStrengthIndex");
            DropColumn("dbo.Securities", "NextDividendAmount");
        }
    }
}
