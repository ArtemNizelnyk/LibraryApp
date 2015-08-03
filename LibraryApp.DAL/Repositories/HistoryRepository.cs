using LibraryApp.DAL.EF;
using LibraryApp.DAL.Interfaces;
using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Repositories
{
    public class HistoryRepository:IRepository<History>
    {
        private LibraryContext db;
 
        public HistoryRepository(LibraryContext context)
        {
            this.db = context;
        }

        public IEnumerable<History> GetAll()
        {
            return db.Historys;
        }

        public History Get(int id)
        {
            return db.Historys.Find(id);
        }

        public void Create(History item)
        {
            db.Historys.Add(item);
        }

        public void Update(History item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<History> Find(Func<History, Boolean> predicate)
        {
            return db.Historys.Where(predicate).ToList();
        }
 
        public void Delete(int id)
        {
            History item = db.Historys.Find(id);
            if (item != null)
                db.Historys.Remove(item);
        }
    }
}
