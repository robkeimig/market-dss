namespace MarketDss.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExDateToSecurity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "NextExDividendDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "NextExDividendDate");
        }
    }
}
