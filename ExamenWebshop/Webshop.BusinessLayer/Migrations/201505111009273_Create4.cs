namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasketItems", "Amount");
        }
    }
}
