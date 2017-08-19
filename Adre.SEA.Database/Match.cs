using Adre.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adre.SEA.Database
{
    public class Match : IMatch
    {
        public Match()
        {
            Id = Guid.NewGuid();
            Athletes = new List<Athlete>();
            DateTimeStart = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime DateTimeStart { get; set; }
        
        public int MatchNo { get; set; }

        public string Arena { get; set; }
        
        public string Venue { get; set; }
        
        public string Remarks { get; set; }

        public virtual Event Event { get; set; }

        public virtual Phase Phase { get; set; }

        public virtual List<Athlete> Athletes { get; set; }

        public virtual Result Result { get; set; }

    }
}
