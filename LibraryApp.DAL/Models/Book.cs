using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [Display(Name="Title")]
        public string Name { get; set; }

        public virtual ICollection<BookAuthors> Authors { get; set; }

        public virtual ICollection<User> Readers { get; set; }

        public virtual ICollection<History> Histories { get; set; }

        public bool IsAvailable { get; set; }

       




    }
}
