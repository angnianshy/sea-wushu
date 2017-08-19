using Adre.Controls;
using System;
using System.ComponentModel.DataAnnotations;

namespace Adre.SEA.Database
{
    public class Ranking : IRanking
    {

        [Key]
        public Guid Id { get; set; }

        public virtual Event Event { get; set; }

        public virtual Contingent Contingent { get; set; }

        public virtual Athlete Athlete { get; set; }

        public string Medal { get; set; }
        
        public float FinalScore { get; set; }

        public int Order { get; set; }

        public virtual IEvent IEvent { get => Event; set => Event = (Event)value; }

        public virtual IContingent IContingent { get => Contingent; set => Contingent = (Contingent)value; }

        public virtual IAthlete IAthlete { get => Athlete; set => Athlete = (Athlete)value; }
        
    }
}
