namespace MarketDss.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Sector = c.String(),
                        PriceToEarningsRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceLow52Weeks = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceHigh52Weeks = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarketCapitalization = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityDividends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityId = c.Int(nullable: false),
                        Dividend = c.Double(nullable: false),
                        ExDividendDate = c.DateTime(nullable: false),
                        RecordDate = c.DateTime(nullable: false),
                        AnnouncementDate = c.DateTime(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityDailyPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Open = c.Double(nullable: false),
                        Close = c.Double(nullable: false),
                        Low = c.Double(nullable: false),
                        High = c.Double(nullable: false),
                        Volume = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
            DropTable("dbo.SecurityDailyPrices");
            DropTable("dbo.SecurityDividends");
            DropTable("dbo.Securities");
        }
    }
}
