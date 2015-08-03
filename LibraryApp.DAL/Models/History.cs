using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    [Table("History")]
    public class History
    {
        [Key]
        public int HistoryId { get; set; }

        
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Picked { get; set; }

        public int UserId { get; set; }
        [Display(Name="Reader")]
        [ForeignKey("UserId")]
        public User WhoPicked { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Returned { get; set; }


    }
}
