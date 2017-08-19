using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adre.Controls
{
    public interface IAthlete
    {
        Guid Id { get; }

        string PreferredName { get; set; }

        string BibNo { get; set; }

        string ToString();

        IContingent IContingent { get;  }
        
        List<IEvent> IEvents { get; }

        string Gender { get; }
    }
}
