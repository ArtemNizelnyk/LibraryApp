using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
        
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public DateTime? Date { get; set; }
    }
}
