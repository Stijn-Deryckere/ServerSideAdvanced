namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BasketItems", "RentPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BasketItems", "RentPrice", c => c.Int(nullable: false));
        }
    }
}
