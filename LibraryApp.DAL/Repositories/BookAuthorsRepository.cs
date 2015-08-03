using LibraryApp.DAL.EF;
using LibraryApp.DAL.Interfaces;
using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Repositories
{
    public class BookAuthorsRepository: IRepository<BookAuthors>
    {
        private LibraryContext db;
 
        public BookAuthorsRepository(LibraryContext context)
        {
            this.db = context;
        }
        public IEnumerable<BookAuthors> GetAll()
        {
            return db.BookAuthors;
        }

        public BookAuthors Get(int id)
        {
           return db.BookAuthors.Find(id);
        }

        public IEnumerable<BookAuthors> Find(Func<BookAuthors, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(BookAuthors item)
        {
            throw new NotImplementedException();
        }

        public void Update(BookAuthors item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
