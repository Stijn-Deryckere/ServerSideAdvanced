namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Devices", "NewFramework_ID", "dbo.Frameworks");
            DropForeignKey("dbo.Devices", "NewOS_ID", "dbo.OS");
            DropIndex("dbo.Devices", new[] { "NewFramework_ID" });
            DropIndex("dbo.Devices", new[] { "NewOS_ID" });
            AddColumn("dbo.ApplicationUsers", "Name", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Firstname", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Address", c => c.String());
            AddColumn("dbo.ApplicationUsers", "City", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Country", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Zipcode", c => c.String());
            AddColumn("dbo.Devices", "PurchasePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Devices", "RentingPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Devices", "Image", c => c.String());
            AddColumn("dbo.Frameworks", "Device_ID", c => c.Int());
            AddColumn("dbo.OS", "Device_ID", c => c.Int());
            CreateIndex("dbo.Frameworks", "Device_ID");
            CreateIndex("dbo.OS", "Device_ID");
            AddForeignKey("dbo.Frameworks", "Device_ID", "dbo.Devices", "ID");
            AddForeignKey("dbo.OS", "Device_ID", "dbo.Devices", "ID");
            DropColumn("dbo.Devices", "RentPrice");
            DropColumn("dbo.Devices", "Picture");
            DropColumn("dbo.Devices", "NewFramework_ID");
            DropColumn("dbo.Devices", "NewOS_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "NewOS_ID", c => c.Int());
            AddColumn("dbo.Devices", "NewFramework_ID", c => c.Int());
            AddColumn("dbo.Devices", "Picture", c => c.String());
            AddColumn("dbo.Devices", "RentPrice", c => c.Single(nullable: false));
            DropForeignKey("dbo.OS", "Device_ID", "dbo.Devices");
            DropForeignKey("dbo.Frameworks", "Device_ID", "dbo.Devices");
            DropIndex("dbo.OS", new[] { "Device_ID" });
            DropIndex("dbo.Frameworks", new[] { "Device_ID" });
            DropColumn("dbo.OS", "Device_ID");
            DropColumn("dbo.Frameworks", "Device_ID");
            DropColumn("dbo.Devices", "Image");
            DropColumn("dbo.Devices", "RentingPrice");
            DropColumn("dbo.Devices", "PurchasePrice");
            DropColumn("dbo.ApplicationUsers", "Zipcode");
            DropColumn("dbo.ApplicationUsers", "Country");
            DropColumn("dbo.ApplicationUsers", "City");
            DropColumn("dbo.ApplicationUsers", "Address");
            DropColumn("dbo.ApplicationUsers", "Firstname");
            DropColumn("dbo.ApplicationUsers", "Name");
            CreateIndex("dbo.Devices", "NewOS_ID");
            CreateIndex("dbo.Devices", "NewFramework_ID");
            AddForeignKey("dbo.Devices", "NewOS_ID", "dbo.OS", "ID");
            AddForeignKey("dbo.Devices", "NewFramework_ID", "dbo.Frameworks", "ID");
        }
    }
}
