using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    public class BookAuthors
    {
        [Key]
        public int Order { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}
