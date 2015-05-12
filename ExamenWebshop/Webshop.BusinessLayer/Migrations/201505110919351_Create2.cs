namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RentPrice = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        NewDevice_ID = c.Int(nullable: false),
                        NewUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.NewDevice_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewUser_Id, cascadeDelete: true)
                .Index(t => t.NewDevice_ID)
                .Index(t => t.NewUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "NewUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BasketItems", "NewDevice_ID", "dbo.Devices");
            DropIndex("dbo.BasketItems", new[] { "NewUser_Id" });
            DropIndex("dbo.BasketItems", new[] { "NewDevice_ID" });
            DropTable("dbo.BasketItems");
        }
    }
}
