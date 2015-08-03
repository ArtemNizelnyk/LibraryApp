using LibraryApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.Interfaces
{
    public interface IService<T> where T:class
    {
        void Create(T itemDto);
        T Get(string item);
        T GetId(int? id);
        IEnumerable<T> GetAll();
        void Dispose();
    }
}
