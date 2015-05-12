namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderLines", "NewUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrderLines", new[] { "NewUser_Id" });
            DropColumn("dbo.OrderLines", "NewUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderLines", "NewUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.OrderLines", "NewUser_Id");
            AddForeignKey("dbo.OrderLines", "NewUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
