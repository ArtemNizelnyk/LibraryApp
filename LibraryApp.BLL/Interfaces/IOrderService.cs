using LibraryApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO itemDto);
        BookDTO GetBook(int? id);
        IEnumerable<BookDTO> GetBooks();
        void Return(int id);
        void Dispose();
    }
}
