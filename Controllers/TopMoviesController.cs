using Movies.Models;
using Movies.Repository;
using NLog;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class TopMoviesController : Controller
    {
        private IDataRepository<Movie> MovieRepository;
        Logger logger = LogManager.GetLogger("DBLogger");
        public TopMoviesController()
        {
            this.MovieRepository = new DataRepository<Movie>();

        }

        public IEnumerable<SelectListItem> GenreList()
        {

            var Genres = MovieRepository.GetAll().Select(m => new { SelectGenre = m.Genre })
                                                  .Distinct().Select(m => new SelectListItem { Value = m.SelectGenre.ToString(), Text = m.SelectGenre }).ToList();
            return Genres;



        }

        // GET: TopMovies
       public ActionResult Index(int pageNumber)
        {

            string Genre = Request.QueryString["Genre"];
            string currentFilter = Request.QueryString["currentFilter"];
            ViewBag.SelectList = GenreList();
            if (  Genre != null && currentFilter != null)

            {
                int pageId;
                /*if (!Int32.TryParse(pageNumber, out pageId))
                    pageId = 1;*/
               
                ViewBag.currentFilter = currentFilter;
                ViewBag.Genre = Genre;

                return ReturnTopMoviesPage(Genre, currentFilter, pageNumber);
            }
            else {
                
                ViewBag.currentFilter = string.Empty;
                ViewBag.Genre = string.Empty;
                return View();
            }
        }

       



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Genre , string currentFilter, int pageNumber = 1)
        {
            ViewBag.SelectList = GenreList();
            if (Genre != null )
            {
               
                ViewBag.currentFilter = Genre;
                ViewBag.Genre = Genre;
                currentFilter = Genre;
                pageNumber = 1;
            }
            else {
               
               
                ViewBag.currentFilter = string.Empty;
                ViewBag.Genre = string.Empty;
                logger.Info(string.Format("Top movies  for genre :{0}", Genre));
                return View();
            }

            return ReturnTopMoviesPage(Genre, currentFilter, pageNumber);
            

        }

         private ActionResult ReturnTopMoviesPage(string Genre, string currentFilter, int pageNumber = 1)
        {
            var movies = MovieRepository.GetAll().Where(m => m.Genre == Genre);
            var moviesList = movies.OrderByDescending(m => m.Rating).ToPagedList(pageNumber, 8);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_TopMoviesView", moviesList);
            }
            return View(moviesList);

        }



    }
}
