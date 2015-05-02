namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb0205154 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "NewUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BasketItems", new[] { "NewUser_Id" });
            AddColumn("dbo.BasketItems", "SessionUser", c => c.String());
            AlterColumn("dbo.BasketItems", "NewUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BasketItems", "NewUser_Id");
            AddForeignKey("dbo.BasketItems", "NewUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "NewUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BasketItems", new[] { "NewUser_Id" });
            AlterColumn("dbo.BasketItems", "NewUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.BasketItems", "SessionUser");
            CreateIndex("dbo.BasketItems", "NewUser_Id");
            AddForeignKey("dbo.BasketItems", "NewUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
