using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.DTO
{
    public class HistoryDTO
    {
        public int HistoryId { get; set; }
        public int BookId { get; set; }
        public DateTime? Picked { get; set; }
        public UserDTO WhoPicked { get; set; }
        public DateTime? Returned { get; set; }
    }
}
