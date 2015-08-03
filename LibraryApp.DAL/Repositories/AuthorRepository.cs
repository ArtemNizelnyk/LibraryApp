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
    public class AuthorRepository:IRepository<Author>
    {
         private LibraryContext db;
 
        public AuthorRepository(LibraryContext context)
        {
            this.db = context;
        }

        public IEnumerable<Author> GetAll()
        {
            return db.Authors;
        }

        public Author Get(int id)
        {
            return db.Authors.Find(id);
        }

        public void Create(Author item)
        {
            db.Authors.Add(item);
        }

        public void Update(Author item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Author> Find(Func<Author, Boolean> predicate)
        {
            return db.Authors.Where(predicate).ToList();
        }
 
        public void Delete(int id)
        {
            Author item = db.Authors.Find(id);
            if (item != null)
                db.Authors.Remove(item);
        }
    }
}
