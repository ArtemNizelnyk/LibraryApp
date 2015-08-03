using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.EF
{
    public class LibraryContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<History> Historys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }

       

        public LibraryContext(string connectionString)
            : base("LibraryContext") { }

        
}
    }
