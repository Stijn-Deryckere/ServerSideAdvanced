namespace Iotshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableCultures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        visitorGUID = c.String(),
                        RentingPrice = c.Double(nullable: false),
                        NewDevice_ID = c.Int(nullable: false),
                        NewUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.NewDevice_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewUser_Id)
                .Index(t => t.NewDevice_ID)
                .Index(t => t.NewUser_Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PurchasePrice = c.Double(nullable: false),
                        RentingPrice = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Firstname = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Zipcode = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        NewFormTopic_ID = c.Int(),
                        NewOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FormTopics", t => t.NewFormTopic_ID)
                .ForeignKey("dbo.Orders", t => t.NewOrder_ID)
                .Index(t => t.NewFormTopic_ID)
                .Index(t => t.NewOrder_ID);
            
            CreateTable(
                "dbo.FormTopics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        RentingPrice = c.Double(nullable: false),
                        NewDevice_ID = c.Int(nullable: false),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.NewDevice_ID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.NewDevice_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.FrameworkDevices",
                c => new
                    {
                        Framework_ID = c.Int(nullable: false),
                        Device_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Framework_ID, t.Device_ID })
                .ForeignKey("dbo.Frameworks", t => t.Framework_ID, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.Device_ID, cascadeDelete: true)
                .Index(t => t.Framework_ID)
                .Index(t => t.Device_ID);
            
            CreateTable(
                "dbo.OSDevices",
                c => new
                    {
                        OS_ID = c.Int(nullable: false),
                        Device_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OS_ID, t.Device_ID })
                .ForeignKey("dbo.OS", t => t.OS_ID, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.Device_ID, cascadeDelete: true)
                .Index(t => t.OS_ID)
                .Index(t => t.Device_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Forms", "NewOrder_ID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "NewUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderLines", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "NewDevice_ID", "dbo.Devices");
            DropForeignKey("dbo.Forms", "NewFormTopic_ID", "dbo.FormTopics");
            DropForeignKey("dbo.BasketItems", "NewUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BasketItems", "NewDevice_ID", "dbo.Devices");
            DropForeignKey("dbo.OSDevices", "Device_ID", "dbo.Devices");
            DropForeignKey("dbo.OSDevices", "OS_ID", "dbo.OS");
            DropForeignKey("dbo.FrameworkDevices", "Device_ID", "dbo.Devices");
            DropForeignKey("dbo.FrameworkDevices", "Framework_ID", "dbo.Frameworks");
            DropIndex("dbo.OSDevices", new[] { "Device_ID" });
            DropIndex("dbo.OSDevices", new[] { "OS_ID" });
            DropIndex("dbo.FrameworkDevices", new[] { "Device_ID" });
            DropIndex("dbo.FrameworkDevices", new[] { "Framework_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderLines", new[] { "Order_ID" });
            DropIndex("dbo.OrderLines", new[] { "NewDevice_ID" });
            DropIndex("dbo.Orders", new[] { "NewUser_Id" });
            DropIndex("dbo.Forms", new[] { "NewOrder_ID" });
            DropIndex("dbo.Forms", new[] { "NewFormTopic_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BasketItems", new[] { "NewUser_Id" });
            DropIndex("dbo.BasketItems", new[] { "NewDevice_ID" });
            DropTable("dbo.OSDevices");
            DropTable("dbo.FrameworkDevices");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
            DropTable("dbo.FormTopics");
            DropTable("dbo.Forms");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.OS");
            DropTable("dbo.Frameworks");
            DropTable("dbo.Devices");
            DropTable("dbo.BasketItems");
            DropTable("dbo.AvailableCultures");
        }
    }
}
