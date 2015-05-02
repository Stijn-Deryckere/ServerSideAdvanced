namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb0205157 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        NewFormTopic_ID = c.Int(nullable: false),
                        NewOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FormTopics", t => t.NewFormTopic_ID, cascadeDelete: true)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forms", "NewOrder_ID", "dbo.Orders");
            DropForeignKey("dbo.Forms", "NewFormTopic_ID", "dbo.FormTopics");
            DropIndex("dbo.Forms", new[] { "NewOrder_ID" });
            DropIndex("dbo.Forms", new[] { "NewFormTopic_ID" });
            DropTable("dbo.FormTopics");
            DropTable("dbo.Forms");
        }
    }
}
