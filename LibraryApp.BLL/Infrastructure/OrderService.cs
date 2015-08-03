using AutoMapper;
using LibraryApp.BLL.DTO;
using LibraryApp.BLL.Exceptions;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Interfaces;
using LibraryApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.Services
{
    public class OrderService:IOrderService
    {
        IUnitOfWork Database { get; set; }
 
        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeOrder(OrderDTO orderDto)
        {
            Book book = Database.Books.Get(orderDto.BookId);
 
            // валидация
            if (book == null)
                throw new ValidationException("Книга не найдена","");
            book.IsAvailable = false;
            Database.Books.Update(book);

            Order order = new Order
            {
                Date = DateTime.Now, 
                BookId=orderDto.BookId,
                UserId=orderDto.UserId
            };
            Database.Orders.Create(order);
            Database.Save();
        }
 
        public IEnumerable<BookDTO> GetBooks()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            Mapper.CreateMap<Book, BookDTO>();
            return Mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetAll());
        }
 
        public BookDTO GetBook(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id книги","");
            var book = Database.Books.Get(id.Value);
            if (book == null)
                throw new ValidationException("Книга не найдена","");
            // применяем автомаппер для проекции Book на BookDTO
            Mapper.CreateMap<Book, BookDTO>();
            Mapper.CreateMap<BookAuthors, BookAuthorsDTO>();
            Mapper.CreateMap<Author, AuthorDTO>();
                
            return Mapper.Map<Book, BookDTO>(book);
        }

        public void Return(int id)
        {
            var book = Database.Books.Get(id);
            book.IsAvailable = true;
            Database.Books.Update(book);
            Database.Save();
        }
 
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
