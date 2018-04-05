namespace MarketDss.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSymbolColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SecurityDividends", "Symbol");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecurityDividends", "Symbol", c => c.String());
        }
    }
}
