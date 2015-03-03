namespace Week2Oefening1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Azure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PurchasePrice = c.Double(nullable: false),
                        RentingPrice = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                        Zipcode = c.String(),
                        City = c.String(),
                        Country = c.String(),
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
                "dbo.FrameworkDevices",
                c => new
                    {
                        Framework_Id = c.Int(nullable: false),
                        Device_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Framework_Id, t.Device_Id })
                .ForeignKey("dbo.Frameworks", t => t.Framework_Id, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.Device_Id, cascadeDelete: true)
                .Index(t => t.Framework_Id)
                .Index(t => t.Device_Id);
            
            CreateTable(
                "dbo.OSDevices",
                c => new
                    {
                        OS_Id = c.Int(nullable: false),
                        Device_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OS_Id, t.Device_Id })
                .ForeignKey("dbo.OS", t => t.OS_Id, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.Device_Id, cascadeDelete: true)
                .Index(t => t.OS_Id)
                .Index(t => t.Device_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OSDevices", "Device_Id", "dbo.Devices");
            DropForeignKey("dbo.OSDevices", "OS_Id", "dbo.OS");
            DropForeignKey("dbo.FrameworkDevices", "Device_Id", "dbo.Devices");
            DropForeignKey("dbo.FrameworkDevices", "Framework_Id", "dbo.Frameworks");
            DropIndex("dbo.OSDevices", new[] { "Device_Id" });
            DropIndex("dbo.OSDevices", new[] { "OS_Id" });
            DropIndex("dbo.FrameworkDevices", new[] { "Device_Id" });
            DropIndex("dbo.FrameworkDevices", new[] { "Framework_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.OSDevices");
            DropTable("dbo.FrameworkDevices");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OS");
            DropTable("dbo.Frameworks");
            DropTable("dbo.Devices");
        }
    }
}
