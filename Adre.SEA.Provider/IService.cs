using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adre.SEA.Provider
{
    public interface IService
    {
        void Save();

        void Load();

        void Register(object context);

        void Refresh();
    }
}
