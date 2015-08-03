using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    [Table("Users")]
    public class User
    {

        [Key]
        public int UserId { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(16)]
        public string Password { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
