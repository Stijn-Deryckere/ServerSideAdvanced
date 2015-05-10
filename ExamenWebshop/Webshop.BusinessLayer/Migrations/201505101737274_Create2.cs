namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RentPrice = c.Double(nullable: false),
                        PurchasePrice = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Picture = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Device_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.Device_ID)
                .Index(t => t.Device_ID);
            
            CreateTable(
                "dbo.OS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Device_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.Device_ID)
                .Index(t => t.Device_ID);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OS", "Device_ID", "dbo.Devices");
            DropForeignKey("dbo.Frameworks", "Device_ID", "dbo.Devices");
            DropIndex("dbo.OS", new[] { "Device_ID" });
            DropIndex("dbo.Frameworks", new[] { "Device_ID" });
            DropColumn("dbo.AspNetUsers", "Zipcode");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.OS");
            DropTable("dbo.Frameworks");
            DropTable("dbo.Devices");
        }
    }
}
