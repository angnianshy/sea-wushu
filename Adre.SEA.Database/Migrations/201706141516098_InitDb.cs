namespace Adre.SEA.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Athletes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        PreferredName = c.String(),
                        Gender = c.String(),
                        UCICode = c.String(),
                        WslId = c.Int(nullable: false),
                        Athlete_Id = c.Guid(),
                        Contingent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id)
                .ForeignKey("dbo.Contingents", t => t.Contingent_Id)
                .Index(t => t.Athlete_Id)
                .Index(t => t.Contingent_Id);
            
            CreateTable(
                "dbo.Contingents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        WslId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        Gender = c.String(),
                        WslId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateTimeStart = c.DateTime(nullable: false),
                        MatchNo = c.Int(nullable: false),
                        Arena = c.String(),
                        Venue = c.String(),
                        Remarks = c.String(),
                        Event_Id = c.Guid(),
                        Phase_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.Phases", t => t.Phase_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Phase_Id);
            
            CreateTable(
                "dbo.Phases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QM = c.Single(nullable: false),
                        OB = c.Single(nullable: false),
                        DD = c.Single(nullable: false),
                        Penalty = c.Single(nullable: false),
                        FinalScore = c.Single(nullable: false),
                        Remarks = c.String(),
                        Match_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.Match_Id)
                .Index(t => t.Match_Id);
            
            CreateTable(
                "dbo.Rankings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Medal = c.String(),
                        FinalScore = c.Single(nullable: false),
                        Order = c.Int(nullable: false),
                        Athlete_Id = c.Guid(),
                        Contingent_Id = c.Guid(),
                        Event_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id)
                .ForeignKey("dbo.Contingents", t => t.Contingent_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Athlete_Id)
                .Index(t => t.Contingent_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.EventAthletes",
                c => new
                    {
                        Event_Id = c.Guid(nullable: false),
                        Athlete_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Athlete_Id })
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Athlete_Id);
            
            CreateTable(
                "dbo.MatchAthletes",
                c => new
                    {
                        Match_Id = c.Guid(nullable: false),
                        Athlete_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Match_Id, t.Athlete_Id })
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .ForeignKey("dbo.Athletes", t => t.Athlete_Id, cascadeDelete: true)
                .Index(t => t.Match_Id)
                .Index(t => t.Athlete_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rankings", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Rankings", "Contingent_Id", "dbo.Contingents");
            DropForeignKey("dbo.Rankings", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.Results", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.Matches", "Phase_Id", "dbo.Phases");
            DropForeignKey("dbo.Matches", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.MatchAthletes", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.MatchAthletes", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.EventAthletes", "Athlete_Id", "dbo.Athletes");
            DropForeignKey("dbo.EventAthletes", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Athletes", "Contingent_Id", "dbo.Contingents");
            DropForeignKey("dbo.Athletes", "Athlete_Id", "dbo.Athletes");
            DropIndex("dbo.MatchAthletes", new[] { "Athlete_Id" });
            DropIndex("dbo.MatchAthletes", new[] { "Match_Id" });
            DropIndex("dbo.EventAthletes", new[] { "Athlete_Id" });
            DropIndex("dbo.EventAthletes", new[] { "Event_Id" });
            DropIndex("dbo.Rankings", new[] { "Event_Id" });
            DropIndex("dbo.Rankings", new[] { "Contingent_Id" });
            DropIndex("dbo.Rankings", new[] { "Athlete_Id" });
            DropIndex("dbo.Results", new[] { "Match_Id" });
            DropIndex("dbo.Matches", new[] { "Phase_Id" });
            DropIndex("dbo.Matches", new[] { "Event_Id" });
            DropIndex("dbo.Athletes", new[] { "Contingent_Id" });
            DropIndex("dbo.Athletes", new[] { "Athlete_Id" });
            DropTable("dbo.MatchAthletes");
            DropTable("dbo.EventAthletes");
            DropTable("dbo.Rankings");
            DropTable("dbo.Results");
            DropTable("dbo.Phases");
            DropTable("dbo.Matches");
            DropTable("dbo.Events");
            DropTable("dbo.Contingents");
            DropTable("dbo.Athletes");
        }
    }
}
