using Catherine_Movie.BusinessRules;
using DAO.Models;
using System.Web.Mvc;

namespace Catherine_Movie.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            //valida formulário
            if (!ModelState.IsValid)
                return View();

            //valida se usuário já existe, se existe manda para o formulário de login
            if(!UserBusinessRules.VerifyUserCont(model.Name))
                return RedirectToAction("Index", "Login");

            //persiste registro
            DAO.Persistence.UserPersistence.CreateUser(model);

            return RedirectToAction("Index", "Login");
        }
    }
}