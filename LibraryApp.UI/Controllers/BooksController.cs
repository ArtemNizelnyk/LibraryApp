using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryApp.BLL.Interfaces;
using LibraryApp.BLL.DTO;
using LibraryApp.UI.Models;
using AutoMapper;



namespace LibraryApp.UI.Controllers
{
    public class BooksController : Controller
    {
        IService<BookDTO> bookService;
        public BooksController(IService<BookDTO> serv)
        {
            bookService = serv;
        }
        // GET: Books
        
        public ActionResult Index(string message = "Library")
        {
            var userId = Request.Cookies.Get("userInfo").Values["userId"].ToString();
            if (userId == "undefined")
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Name = message;
            return View();
        }

        public JsonResult GetBooks(string sidx, string sord, int page, int rows)  
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            //Mapping Моделей и подтягивание имен авторов
            Mapper.CreateMap<BookDTO, BookViewModel>();
            Mapper.CreateMap<BookAuthorsDTO, AuthorViewModel>()
                    .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.AuthorId));
            Mapper.CreateMap<BookViewModel, BookDTO>()
                  .AfterMap((s, d) =>
                  {
                      foreach (var bookAuthor in d.Authors)
                      {
                          bookAuthor.BookId = s.BookId;
                          bookAuthor.Author.Name = s.Authors.Select(p => p.Author.Name).ToString(); ;
                      }

                  });
            Mapper.CreateMap<AuthorViewModel, BookAuthorsDTO>()
                  .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.AuthorId));
            Mapper.CreateMap<BookAuthorsDTO, AuthorDTO>();
            var books = Mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookService.GetAll());
            
            //Список для возврата в формате JSON
            var BooksResults = books.Select(a => new
            {
                a.BookId,
                a.Name,
                a.IsAvailable,
                AuthorNames = a.Authors.Select(p=>p.Author.Name)
            });
            int totalRecords = BooksResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
           
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = BooksResults
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            Mapper.CreateMap<BookDTO, BookViewModel>();
            var book = Mapper.Map<BookDTO, BookViewModel>(bookService.GetId(id));

            return View(book);
        }

        
    }
}
