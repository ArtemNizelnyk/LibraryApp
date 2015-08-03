using AutoMapper;
using LibraryApp.BLL.DTO;
using LibraryApp.BLL.Exceptions;
using LibraryApp.BLL.Interfaces;
using LibraryApp.DAL.Interfaces;
using LibraryApp.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.BLL.Infrastructure
{
    public class BookService:IService<BookDTO>
    {
        IUnitOfWork Database { get; set; }
 
        public BookService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Create(BookDTO bookDto)
        {
            Book book = Database.Books.Find(o=>o.Name==bookDto.Name).First();
 
            // валидация
            if (book == null)
            {
                Book newBook = new Book
                 {
                     Name = bookDto.Name,
                     Authors = (ICollection<BookAuthors>)bookDto.Authors,
                     IsAvailable = true
                 };


                Database.Books.Create(newBook);
            }
            else
            {
                book.Name=bookDto.Name;
                book.Authors = (ICollection < BookAuthors >)bookDto.Authors;
                book.IsAvailable = bookDto.IsAvailable;
                book.Readers = bookDto.Readers;
                book.Histories = bookDto.Histories;

                Database.Books.Update(book);
            }

            Database.Save();
        }
 
        public IEnumerable<BookDTO> GetAll()
        {
            Mapper.CreateMap<Book, BookDTO>();

            Mapper.CreateMap<BookAuthors, AuthorDTO>()
                    .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.AuthorId));

            Mapper.CreateMap<BookDTO, Book>()
                  .AfterMap((s, d) =>
                  {
                      foreach (var bookAuthor in d.Authors)
                      { bookAuthor.BookId = s.BookId;
                      bookAuthor.Author.Name = s.Authors.Select(p => p.Author.Name).ToString();
                      }
                            
                  });
            Mapper.CreateMap<Author, AuthorDTO>();

            Mapper.CreateMap<AuthorDTO, BookAuthors>()
                  .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.AuthorId));
            Mapper.CreateMap<BookAuthors, BookAuthorsDTO>();
            
            return Mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetAll());
        }

        public BookDTO GetId(int? id)
        {
            if (id == null)
                throw new ValidationException("Book's ID not installed", "");
            var book = Database.Books.Get(id.Value);
            if (book == null)
                throw new ValidationException("Book wasn't founded", "");
            // применяем автомаппер для проекции Book на BookDTO
            Mapper.CreateMap<Book, BookDTO>();
            return Mapper.Map<Book, BookDTO>(book);
        }

        public BookDTO Get(string name)
        {
            if (name == null)
                throw new ValidationException("Book's name not installed","");
            var book = Database.Books.Find(o=>o.Name==name).First();
            if (book == null)
                throw new ValidationException("Book wasn't founded", "");
            // применяем автомаппер для проекции Book на BookDTO
            Mapper.CreateMap<Book, BookDTO>();
            return Mapper.Map<Book, BookDTO>(book);
        }
 
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
