using System;

namespace Adre.Controls
{
    public interface IMatch
    {
        Guid Id { get; }

        DateTime DateTimeStart { get; set; }

        int MatchNo { get; set; }

        string Arena { get; set; }

        string Venue { get; set; }
        
        string Remarks { get; set; }
    }
}
