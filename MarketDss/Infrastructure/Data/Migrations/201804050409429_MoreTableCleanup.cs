namespace MarketDss.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreTableCleanup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecurityDividends", "Symbol", c => c.String());
            AlterColumn("dbo.SecurityDividends", "Dividend", c => c.Double());
            AlterColumn("dbo.SecurityDividends", "ExDividendDate", c => c.DateTime());
            AlterColumn("dbo.SecurityDividends", "RecordDate", c => c.DateTime());
            AlterColumn("dbo.SecurityDividends", "AnnouncementDate", c => c.DateTime());
            AlterColumn("dbo.SecurityDividends", "PaymentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecurityDividends", "PaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecurityDividends", "AnnouncementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecurityDividends", "RecordDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecurityDividends", "ExDividendDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SecurityDividends", "Dividend", c => c.Double(nullable: false));
            DropColumn("dbo.SecurityDividends", "Symbol");
        }
    }
}
