using AutoMapper;
using LibraryApp.BLL.DTO;
using LibraryApp.BLL.Exceptions;
using LibraryApp.BLL.Interfaces;
using LibraryApp.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class OrderController : Controller
    {
        IOrderService orderService;
        public OrderController(IOrderService serv)
        {
            orderService = serv;
        }
         //Взять книгу из библиотеки
        public ActionResult MakeOrder(int? id)
        {
            try
            {
                //Проверка на авторизацию
                var userId = Request.Cookies.Get("userInfo").Values["userId"].ToString();
                if (userId=="undefined")
                {
                    return RedirectToAction("Login", "Account");
                }
                Mapper.CreateMap<BookDTO, OrderViewModel>()
                    .ForMember(dst => dst.BookId, opt => opt.MapFrom(src => src.BookId))
                    .ForMember(dst => dst.BookName, opt => opt.MapFrom(src => src.Name));
                
                var order = Mapper.Map<BookDTO, OrderViewModel>(orderService.GetBook(id));
                order.UserId = Int32.Parse(userId);
                order.Date = DateTime.Now;
                return View("MakeOrder",order);
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        //Добавление записи о том что книга была взята пользователем
        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                Mapper.CreateMap<OrderViewModel,OrderDTO>();
                var orderDto = Mapper.Map<OrderViewModel, OrderDTO>(order);
                orderService.MakeOrder(orderDto);
                string s = "Вы успешно взяли книгу: "+ order.BookName;
                return RedirectToAction("Index", "Books", new { message = s });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(order);
        }
        public ActionResult ReturnBook(int id) 
        {
            Mapper.CreateMap<BookDTO, OrderViewModel>()
                    .ForMember(dst => dst.BookId, opt => opt.MapFrom(src => src.BookId))
                    .ForMember(dst => dst.BookName, opt => opt.MapFrom(src => src.Name));

            var order = Mapper.Map<BookDTO, OrderViewModel>(orderService.GetBook(id));
            orderService.Return(id);
            var s = "Вы вернули книгу"+order.BookName;
            
            return RedirectToAction("Index", "Books", new { message = s });
        }

        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }
    }
}