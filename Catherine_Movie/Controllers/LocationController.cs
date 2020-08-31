using DAO.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Catherine_Movie.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Index()
        {
            Movie m = new Movie();
            var listMovies = DAO.Query.MovieQuery.GetMovies(m);
            ViewBag.dropMovies = new SelectList(listMovies.ToList(), "Name", "Name");


            return View();
        }

        public JsonResult NewLocation(string CPF, string Table)
        {

            Location l = new Location();
            l.CPF = CPF;
            int idLo = DAO.Persistence.LocationPersistence.CreateLocation(l);

            Table.Split('|').ToList().ForEach(o => {

                if (o != "") {
                    Movie mo = new Movie();
                    mo.Name = o;
                    int IdMovie = DAO.Query.MovieQuery.GetMovies(mo).First().Id;

                    LocateRegister lr = new LocateRegister();
                    lr.IdMovie = IdMovie;
                    lr.IdLocation = idLo;
                    DAO.Persistence.LocateRegisterPersistence.CreateRegister(lr);
                }
            });

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMovies(string Id)
        {
            ArrayList str = new ArrayList();

            Location l = new Location();
            var listLocations = DAO.Query.LocationQuery.GetLocations();

            LocateRegister lr = new LocateRegister();
            var listLocationRegisters = DAO.Query.LocationQuery.GetLocateRegister(lr);

            Movie m = new Movie();
            var listMovies = DAO.Query.MovieQuery.GetMovies(m);

            var list = listLocations.Join(listLocationRegisters,
            lo => lo.Id,
            lor => lor.IdLocation,
            (lo, lor) => new
            {
                lo.Id,
                lo.CPF,
                lo.LocationDate,
                lor.IdMovie
            }).Join(listMovies,
            lo => lo.IdMovie,
            mo => mo.Id,
            (lo, mo) => new
            {
                lo.Id,
                lo.CPF,
                lo.LocationDate,
                lo.IdMovie,
                mo.Name
            });

            str.Add(new
            {
                list = list.Where(o => o.Id == Convert.ToInt32(Id)).Select(o => o.Name)
            });

            return Json(str, JsonRequestBehavior.AllowGet);
        }    
    }
}
