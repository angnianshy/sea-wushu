namespace Adre.SEA.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMedalColumnToResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "Medal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "Medal");
        }
    }
}
