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
    public class UserService:IService<UserDTO>
    {
        IUnitOfWork Database { get; set; }
 
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Create(UserDTO userDTO)
        {
            User userToCreate = Database.Users.Get(userDTO.UserId);
            // валидация
            if (userToCreate == null)
            {
                User user = new User
                {
                    UserId= userDTO.UserId,
                    Email=userDTO.Email,
                    FirstName=userDTO.FirstName,
                    LastName=userDTO.LastName,
                    Password=userDTO.Password
                };
                Database.Users.Create(user);
                Database.Save();
            }
        }
 
        public IEnumerable<UserDTO> GetAll()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            Mapper.CreateMap<User, UserDTO>();
            return Mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }
 
        public UserDTO Get(string email)
        {
            if (email == null)
                throw new ValidationException("Не установлено id юзера","");
            var user = Database.Users.Find(o=>o.Email==email).First();
            if (user == null)
                throw new ValidationException(" не найден","");
            // применяем автомаппер для проекции User на UserDTO
            Mapper.CreateMap<User, UserDTO>();
            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO GetId(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id юзера", "");
            var user = Database.Users.Get(id.Value);
            if (user == null)
                throw new ValidationException(" не найден", "");
            // применяем автомаппер для проекции User на UserDTO
            Mapper.CreateMap<User, UserDTO>();
            return Mapper.Map<User, UserDTO>(user);
        }
 
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
