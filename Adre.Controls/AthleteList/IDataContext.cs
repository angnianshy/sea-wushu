using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adre.Controls.AthleteList
{
    public interface IDataContext
    {
        ICollection<IAthlete> AthleteList { get; set; }
    }
}
