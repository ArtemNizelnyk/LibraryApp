using LibraryApp.DAL.EF;
using LibraryApp.DAL.Interfaces;
using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL
{
    public class BookRepository:IRepository<Book>
    {
          private LibraryContext db;
 
        public BookRepository(LibraryContext context)
        {
            this.db = context;
        }
 
        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }
 
        public Book Get(int id)
        {
            return db.Books.Find(id);
        }
 
        public void Create(Book book)
        {
            db.Books.Add(book);
        }
 
        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }
 
        public IEnumerable<Book> Find(Func<Book, Boolean> predicate)
        {
            return db.Books.Where(predicate).ToList();
        }
 
        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }
    }
}
