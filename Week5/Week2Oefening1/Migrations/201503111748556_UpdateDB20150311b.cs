namespace Week2Oefening1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB20150311b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsDelivered", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Name", c => c.String());
            AddColumn("dbo.Orders", "FirstName", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Zipcode");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "FirstName");
            DropColumn("dbo.Orders", "Name");
            DropColumn("dbo.Orders", "IsDelivered");
        }
    }
}
