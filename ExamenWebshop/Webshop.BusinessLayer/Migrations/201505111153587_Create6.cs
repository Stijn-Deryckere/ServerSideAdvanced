namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasketItems", "IsActive");
        }
    }
}
