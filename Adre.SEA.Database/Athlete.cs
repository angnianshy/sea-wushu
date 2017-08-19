using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Adre.Controls;

namespace Adre.SEA.Database
{
    public class Athlete : IAthlete
    {
        public Athlete()
        {
            Id = Guid.NewGuid();
            Events = new List<Event>();
            Matches = new List<Match>();
        }

        [Key]
        public Guid Id { get; set; }

        public string FullName { get; set; }
        
        public string PreferredName { get; set; }

        public string Gender { get; set; }

        public string BibNo { get; set; }
        
        public DateTime? DOB { get; set; }

        public virtual List<Event> Events { get; set; }

        public virtual List<Match> Matches { get; set; }

        public virtual List<IEvent> IEvents { get => Events.ConvertAll(m => (IEvent)m); }

        public virtual Contingent Contingent { get; set; }

        public virtual IContingent IContingent { get => Contingent; }

        public override bool Equals(object other)
        {
            var obj = other as Athlete;
            return (obj != null) && Id == obj.Id;
        }

        public override string ToString()
        {
            return PreferredName + " (" + Contingent.Code + ")";
        }

        public int WslId { get; set; }

        //angns
        //public string AthleteRemarks { get; set; }
        //angns
  }
}
