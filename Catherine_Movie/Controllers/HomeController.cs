using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catherine_Movie.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Location l = new Location();
            ViewBag.Locations = DAO.Query.LocationQuery.GetLocations();

            return View();
        }
    }
}