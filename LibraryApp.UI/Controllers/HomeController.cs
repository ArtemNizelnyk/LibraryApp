using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();  
        }

        public ActionResult Main()
        {
            //Создание cookie при первом входе, получение статуса Cookie["isLoged"]
            var cookie = new HttpCookie("userInfo") ;
                cookie.Values["isLoged"] = "False";
                cookie.Values["userId"] = "undefined";
                cookie.Values["lastVisit"] = DateTime.Now.ToString();
                cookie.Expires = DateTime.Now.AddMinutes(30);
            Response.SetCookie(cookie);

                        
            
            if (MyAuthorize()==false)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Books");
            }
        }

        //Проверка на существование Cookie["isLoged"]
        public bool MyAuthorize()
        {
           if (Request.Cookies.Get("userInfo") == null)
               return false;

            if (Request.Cookies.Get("userInfo").Values["isLoged"] == null || Request.Cookies["userInfo"].Values["isLoged"] == "False")
                return false;
            else
                return true;
        }
    }
}