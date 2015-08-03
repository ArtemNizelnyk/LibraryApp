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

namespace LibraryApp.BLL.Infrastructure
{
    public class AuthorService : IService<AuthorDTO>
    {
        IUnitOfWork Database { get; set; }

        public AuthorService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Create(AuthorDTO authorDto)
        {
            Author author = Database.Authors.Find(o => o.Name == authorDto.Name).First();

            // валидация
            if (author == null)
            {
                Author newAuthor = new Author
                {
                    Name = authorDto.Name,
                    Books = (ICollection<BookAuthors>)authorDto.Books
                };


                Database.Authors.Create(newAuthor);
            }
            else
            {
                author.Name = authorDto.Name;
                author.Books =(ICollection<BookAuthors>)authorDto.Books;
                Mapper.CreateMap<Author, AuthorDTO>();

                Database.Authors.Update(author);
            }

            Database.Save();
        }

        public IEnumerable<AuthorDTO> GetAll()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            
            return Mapper.Map<IEnumerable<Author>, List<AuthorDTO>>(Database.Authors.GetAll());
        }

        public AuthorDTO GetId(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено name книги", "");
            var author = Database.Authors.Get(id.Value);
            if (author == null)
                throw new ValidationException("Книга не найдена", "");
            // применяем автомаппер для проекции Book на BookDTO
            Mapper.CreateMap<Author, AuthorDTO>();
            return Mapper.Map<Author, AuthorDTO>(author);
        }

        public AuthorDTO Get(string name)
        {
            if (name == null)
                throw new ValidationException("Не установлено name книги", "");
            var author = Database.Authors.Find(o => o.Name == name).First();
            if (author == null)
                throw new ValidationException("Книга не найдена", "");
            // применяем автомаппер для проекции Book на BookDTO
            Mapper.CreateMap<Author, AuthorDTO>();
            return Mapper.Map<Author, AuthorDTO>(author);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
