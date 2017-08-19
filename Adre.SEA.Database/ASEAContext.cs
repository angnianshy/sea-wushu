using System.Data.Entity;

namespace Adre.SEA.Database
{
    public class ASEAContext : DbContext
    {
        public ASEAContext() : base("ASEAContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Match>()
                .HasOptional(s => s.Result)
                .WithOptionalPrincipal(s => s.Match);
        }

        public virtual DbSet<Athlete> Athletes { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Match> Matches { get; set; }

        public virtual DbSet<Phase> Phases { get; set; }

        public virtual DbSet<Contingent> Contingents { get; set; }
        
        public virtual DbSet<Result> Result { get; set; }
        
        public virtual DbSet<Ranking> Rankings { get; set; }
    }
}
