namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RentPrice = c.Double(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        NewDevice_ID = c.Int(nullable: false),
                        NewUser_Id = c.String(nullable: false, maxLength: 128),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.NewDevice_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.NewDevice_ID)
                .Index(t => t.NewUser_Id)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        NewUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.NewUser_Id, cascadeDelete: true)
                .Index(t => t.NewUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "NewUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderLines", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "NewUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices");
            DropIndex("dbo.Orders", new[] { "NewUser_Id" });
            DropIndex("dbo.OrderLines", new[] { "Order_ID" });
            DropIndex("dbo.OrderLines", new[] { "NewUser_Id" });
            DropIndex("dbo.OrderLines", new[] { "NewDevice_ID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
        }
    }
}
