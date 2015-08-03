using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.DTO
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BookAuthorsDTO> Books { get; set; }
    }
}
