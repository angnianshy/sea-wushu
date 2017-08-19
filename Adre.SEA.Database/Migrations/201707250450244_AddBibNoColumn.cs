namespace Adre.SEA.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBibNoColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Athletes", "BibNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Athletes", "BibNo");
        }
    }
}
