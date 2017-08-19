using Adre.Controls;
using Adre.Controls.AthleteList;
using Adre.SEA.Database;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Adre.SEA.Provider
{
    public class AthleteService : IService
    {
        IDataContext _context;
        ASEAContext _dbContext;

        public AthleteService()
        {
            _dbContext = new ASEAContext();
        }

        public void Load()
        {
            _context.AthleteList = new ObservableCollection<IAthlete>(_dbContext.Athletes.OrderBy(m => m.Contingent.Code).ThenBy(m => m.PreferredName));
        }

        public void Register(object context)
        {
            _context = (IDataContext)context;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Refresh()
        {
            _dbContext = new ASEAContext();
            Load();
        }

        public void Update(Guid id, object athleteObj)
        {
            var athlete = athleteObj as IAthlete;
            var obj = _dbContext.Athletes.Find(id);

            if (obj == null) return;

            obj.BibNo = athlete.BibNo;

            _dbContext.SaveChanges();
        }
    }
}
