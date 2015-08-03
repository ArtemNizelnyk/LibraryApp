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
    public class AccountController : Controller
    {
        IService<UserDTO> userService;
        public AccountController(IService<UserDTO> serv)
        {
            userService = serv;
        }

        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {

           
                try
                {
                    Mapper.CreateMap<UserViewModel, UserDTO>();
                    var userDto = Mapper.Map<UserViewModel, UserDTO>(model);
                    var userModel= userService.Get(userDto.Email);
                    if (model.Password == userModel.Password)
                    {
                        //Авторизация и добавление cookie
                        HttpCookie aCookie = Request.Cookies["userInfo"];
                        aCookie.Values["isLoged"] = "True";
                        aCookie.Values["userId"] = userModel.UserId.ToString();
                        aCookie.Values["userEmail"] = userModel.Email.ToString();
                        Response.SetCookie(aCookie);

                        return RedirectToAction("Index", "Books");
                    }
                    else 
                    {
                        throw new ValidationException("Incorect pair of email and password", "");
                    }
                    
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            
            return View(model);
        }
 
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                var user = new HttpCookie("userInfo")
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    Value = null
                };
                //Получение статуса LogOut
                user.Values["isLoged"] = "False";
                user.Values["userId"] = "undefined";
                user.Values["userEmail"] = null;

                Response.SetCookie(user);
            }
            return RedirectToActionPermanent("Login");
        }
 
        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.CreateMap<UserViewModel, UserDTO>();
                    var userDto = Mapper.Map<UserViewModel, UserDTO>(model);
                    userService.Create(userDto);
                    return RedirectToAction("Login");
                }
                catch (ValidationException ex)
                {
                    //вывод ошибок из BLL
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }
    
    }
}