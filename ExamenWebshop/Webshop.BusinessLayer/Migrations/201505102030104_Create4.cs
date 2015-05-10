namespace Webshop.BusinessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "Picture", c => c.String(nullable: false));
        }
    }
}
