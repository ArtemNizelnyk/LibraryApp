using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.DTO
{
    public class BookAuthorsDTO
    {
        public int Order { get; set; }
        public int BookId { get; set; }
        public virtual BookDTO Book { get; set; }
        public virtual AuthorDTO Author { get; set; }
        public int AuthorId { get; set; }


    }
}
