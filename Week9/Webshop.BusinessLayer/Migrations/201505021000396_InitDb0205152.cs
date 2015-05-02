namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb0205152 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices");
            DropIndex("dbo.OrderLines", new[] { "NewDevice_ID" });
            AlterColumn("dbo.OrderLines", "NewDevice_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderLines", "NewDevice_ID");
            AddForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices");
            DropIndex("dbo.OrderLines", new[] { "NewDevice_ID" });
            AlterColumn("dbo.OrderLines", "NewDevice_ID", c => c.Int());
            CreateIndex("dbo.OrderLines", "NewDevice_ID");
            AddForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices", "ID");
        }
    }
}
