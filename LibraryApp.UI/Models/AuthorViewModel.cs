using LibraryApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.UI.Models
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BookAuthorsDTO> Books { get; set; }
    }
}