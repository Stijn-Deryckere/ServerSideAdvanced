namespace ScoreApplicatie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Flag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "Flag");
        }
    }
}
