namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb0205155 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "visitorGUID", c => c.String());
            DropColumn("dbo.BasketItems", "SessionUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BasketItems", "SessionUser", c => c.String());
            DropColumn("dbo.BasketItems", "visitorGUID");
        }
    }
}
