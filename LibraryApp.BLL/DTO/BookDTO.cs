using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookAuthorsDTO> Authors { get; set; }
        public virtual ICollection<User> Readers { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public bool IsAvailable { get; set; }
    }
}
