namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb0205151 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderLines", "RentingPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderLines", "RentingPrice", c => c.Int(nullable: false));
        }
    }
}
