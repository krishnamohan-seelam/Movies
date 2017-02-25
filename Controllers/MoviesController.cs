using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movies.Models;
using PagedList;
using NLog;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        
        Logger logger = LogManager.GetLogger("DBLogger");
        // GET: Movies 
        /*Eager loading ...  using Include() 
         *Lazy loading  ...  using virtual in Model classes
         */

        public ActionResult Index(string search , string currentFilter ,int pageNumber = 1)
        {
           
          /*var movies = (from m in db.Movies select m).Include( m => m.MovieImages) ;*/

          var movies = (from m in db.Movies select m).Include(m => m.MovieImages);

            if (search!=null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }
          
            ViewBag.CurrentFilter = search;

            if (!string.IsNullOrEmpty(search))
            {
                logger.Info(string.Format("Searching for movie word :{0}",search));
                movies = movies.Where(m => m.Title.Contains(search));
            }

           var  moviesList = movies.OrderBy(m=>m.Title).ToPagedList(pageNumber, 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SearchView", moviesList);
            }
            return View(moviesList);
        }

        // AutoSearch

          public ActionResult AutoComplete([Bind(Prefix = "search") ]string term)
        {

            
                var movies = db.Movies.Where(m => m.Title.StartsWith(term))
                                      .Take(10)
                                      .Select(m => new {title = m.Title }).Distinct().ToList();



                 return Json(movies, JsonRequestBehavior.AllowGet);

        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
