using System;
using System.Collections.Generic;
using System.Linq;

namespace Adre.Controls
{
    public interface IResult
    {
        Guid Id { get; set; }

        float QM { get; set; }

        float OB { get; set; }

        float DD { get; set; }

        float Penalty { get; set; }

        float FinalScore { get; set; }

        string Remarks { get; set; }

        string Medal { get; set; } //angns

  }
}
