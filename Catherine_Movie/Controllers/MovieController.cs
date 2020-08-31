using DAO.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catherine_Movie.Controllers
{
    public class MovieController : Controller
    {
        public ActionResult Index()
        {
            //validação de log
            if (ReadUser() == String.Empty)
                return RedirectToAction("Index", "Login");

            // carrega lista para a grid
            Movie m = new Movie();
            ViewBag.Movies = JsonConvert.DeserializeObject<List<MovieGenre>>(MountJoin(m));

            return View();
        }

        /// <summary>
        /// Método para pesquisa 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Grid(string Name, string Type, string Genre, string Id)
        {
            try
            {
                Movie m = new Movie();
                List<MovieGenre> list = null;

                switch (Type)
                {
                    // carrega resultado da pesquisa para a grid
                    case "Search":
                        m.Name = Name;
                        break;

                    // cria novo registro de filme
                    case "Save":
                        m.Name = Name;
                        m.GenreId = Convert.ToInt32(Genre);
                        if (!DAO.Persistence.MoviePersistence.CreateMovie(m))
                            Common.Log.Write("MovieController Grid", "erro ao salvar filme");

                        m = new Movie();
                        break;

                    //deleta filmes selecionados
                    case "Delete":
                        var listMovies = DAO.Query.MovieQuery.GetMovies(m);
                        if (listMovies.Count == 0)
                            Common.Log.Write("MovieController Grid", "erro ao salvar filme");

                        listMovies.Where(o => o.Del == true).ToList().ForEach(o =>
                        {

                            DAO.Delete.MoviesDelete.DeleteMovie(o.Id);
                        });
                        break;

                    // edita filme
                    case "Edit":
                        var listMoviesEdit = DAO.Query.MovieQuery.GetMovies(m).Where(o => o.Id == Convert.ToInt32(Id)).First();
                        listMoviesEdit.Name = Name;
                       
                        DAO.Edit.MovieEdit.EditMovie(listMoviesEdit);
                        break;
                }

                list = JsonConvert.DeserializeObject<List<MovieGenre>>(MountJoin(m));
                ViewBag.Movies = list;
                return PartialView("Grid", list);
            }
            catch (Exception err)
            {
                //escreve log
                Common.Log.Write("MovieController Grid", err.Message);
                return PartialView("Grid", null);
            }
        }

        /// <summary>
        /// verifica se usuário esta no cookie
        /// </summary>
        /// <returns></returns>
        private string ReadUser() {

            if (Request.Cookies["UserData"] != null)
            {
                return Request.Cookies["UserData"]["user"];
            }
            else {

                return String.Empty;
            }
        }

        /// <summary>
        /// monta join para ser usado nas grids
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string MountJoin(Movie m)
        {
            var listMovies = DAO.Query.MovieQuery.GetMovies(m);
            if (listMovies.Count == 0)
                return "";

            Genre g = new Genre();
            var listGenres = DAO.Query.GenreQuery.GetGenres(g);
            if(listGenres.Count == 0)
                return "";

            //preenche dropdown de generos
            ViewBag.dropGenre = new SelectList(listGenres.ToList(), "Id", "Name");

            var list = listMovies.Join(listGenres,
                (mo => mo.GenreId),
                (ge => ge.Id),
                ((mo, ge) => new
                {
                    mo.Id,
                    mo.CreationDate,
                    mo.Name,
                    mo.Active,
                    Genre = ge.Name,
                    mo.Del
                })).ToList();

            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// ajax que muda o status de ativo
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult ChangeActived(string Id)
        {
            ArrayList str = new ArrayList();

            Movie m = new Movie();
            m.Id = Convert.ToInt32(Id);
            var listMovies = DAO.Query.MovieQuery.GetMovies(m);
            
            if (listMovies.Count > 0) {

                var reg = listMovies.First();
                
                if (reg.Active == true)
                {
                    reg.Active = false;
                }
                else
                {
                    reg.Active = true;
                }

                try
                {
                    DAO.Edit.MovieEdit.EditMovie(reg);
                }
                catch (Exception err)
                {
                    //escreve log
                    Common.Log.Write("MovieController Grid", err.Message);
                }

                str.Add(new
                {
                    Active = reg.Active,
                    nome = reg.Name.Replace(" ", "")
                });
            }

            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// cadastro novo gênero
        /// </summary>
        /// <param name="Genre"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NewGenre(string Genre)
        {
            ArrayList str = new ArrayList();

            Genre g = new Genre();
            g.Name = Genre;
            if(!DAO.Persistence.GenrePersistence.CreateGenre(g))
                //escreve log
                Common.Log.Write("MovieController NewGenre", "Erro ao salvar gênero");

            g = new Genre();
            var listGenres = DAO.Query.GenreQuery.GetGenres(g);
            if (listGenres.Count == 0)
                //escreve log
                Common.Log.Write("MovieController NewGenre", "Erro ao fazer query em gênero");

            return Json(new SelectList(listGenres.ToList(), "Id", "Name"));
        }

        /// <summary>
        /// muda status de delete do filme
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult ChangeDeleteMovies(string Id)
        {
            ArrayList str = new ArrayList();

            Movie m = new Movie();
            m.Id = Convert.ToInt32(Id);
            var listMovies = DAO.Query.MovieQuery.GetMovies(m);

            if (listMovies.Count > 0)
            {

                var reg = listMovies.First();

                if (reg.Del == true)
                {
                    reg.Del = false;
                }
                else
                {
                    reg.Del = true;
                }

                try
                {
                    DAO.Edit.MovieEdit.EditMovie(reg);
                }
                catch (Exception err)
                {
                    //escreve log
                    Common.Log.Write("MovieController Grid", err.Message);
                }

                str.Add(new
                {
                    Del = reg.Del,
                    nome = reg.Name.Replace(" ", ""),
                    id = reg.Id
                });
            }

            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// pega filme escolhido na grid
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetMovie(string Id)
        {
            ArrayList str = new ArrayList();

            Movie m = new Movie();
            m.Id = Convert.ToInt32(Id);
            var listMovies = DAO.Query.MovieQuery.GetMovies(m);

            if (listMovies.Count > 0)
            {
                str.Add(new
                {
                    listMovies.First().Name,
                    listMovies.First().Id,
                });
            }

            return Json(str, JsonRequestBehavior.AllowGet);
        }
    }

    public class MovieGenre
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Active { get; set; }

        public string Genre { get; set; }
        public bool Del { get; set; }
    }
}