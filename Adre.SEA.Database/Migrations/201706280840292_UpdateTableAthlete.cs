namespace Adre.SEA.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableAthlete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Athletes", "Athlete_Id", "dbo.Athletes");
            DropIndex("dbo.Athletes", new[] { "Athlete_Id" });
            AddColumn("dbo.Athletes", "DOB", c => c.DateTime());
            DropColumn("dbo.Athletes", "UCICode");
            DropColumn("dbo.Athletes", "Athlete_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Athletes", "Athlete_Id", c => c.Guid());
            AddColumn("dbo.Athletes", "UCICode", c => c.String());
            DropColumn("dbo.Athletes", "DOB");
            CreateIndex("dbo.Athletes", "Athlete_Id");
            AddForeignKey("dbo.Athletes", "Athlete_Id", "dbo.Athletes", "Id");
        }
    }
}
