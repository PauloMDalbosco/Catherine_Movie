using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catherine_Movie.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Password, string Email)
        {
            //monta entidade
            User u = new User();
            u.Email = Email;
            u.Password = Common.MD5HashClass.Generate(Password);

            //verifica se existe o usuário e senha
            List<User> list = DAO.Query.UserQuery.GetUser(u);

            if (list.Count > 0)
            {
                //guarda dados do usuário em cookies
                HttpCookie cookiesUser = new HttpCookie("UserData");
                cookiesUser["user"] = list.First().Name;
                cookiesUser.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(cookiesUser);

                return RedirectToAction("Index", "Home");
            }
            else {

                ViewBag.errlogin = "Nome ou senha não coincidem.";
                return View();
            }
        }

        public ActionResult ClearData()
        {
            if (Request.Cookies["UserData"] != null)
            {
                Response.Cookies["UserData"].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}