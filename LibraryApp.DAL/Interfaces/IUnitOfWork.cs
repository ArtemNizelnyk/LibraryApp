using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Interfaces
{
   public  interface IUnitOfWork:IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }
        IRepository<History> Historys { get; }
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
