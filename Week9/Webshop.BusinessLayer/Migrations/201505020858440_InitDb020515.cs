namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb020515 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        RentingPrice = c.Int(nullable: false),
                        NewDevice_ID = c.Int(),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.NewDevice_ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.NewDevice_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices");
            DropIndex("dbo.Orders", new[] { "NewUser_Id" });
            DropIndex("dbo.OrderLines", new[] { "Order_ID" });
            DropIndex("dbo.OrderLines", new[] { "NewDevice_ID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
        }
    }
}
