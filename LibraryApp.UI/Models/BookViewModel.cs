using LibraryApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApp.UI.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BookAuthorsDTO> Authors { get; set; }
        public virtual ICollection<UserViewModel> Readers { get; set; }
        public virtual ICollection<HistoryViewModel> Historys { get; set; }
        public bool IsAvailable { get; set; }
    }
}