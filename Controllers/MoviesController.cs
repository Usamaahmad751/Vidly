using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _contact;
        public MoviesController()
        {

            _contact = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

        }
        // GET: Movies
        public ActionResult Index()
        {

            var Movies = _contact.Movies.ToList();
            return View(Movies);

        }

        // Create New Movie
        public ActionResult New()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            _contact.Movies.Add(movie);
           
                _contact.SaveChanges();
           
            return RedirectToAction("Index","Movies");
        }
      
        public ActionResult Edit(Movie movie)
        {
            var Movie = _contact.Movies.SingleOrDefault(c => c.Id == movie.Id);
            if (Movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                Movie.Name = movie.Name;
                Movie.Genere = movie.Genere
                    ;
            }
            _contact.SaveChanges();
            ;

            

            return RedirectToAction("Index");
        }





        public ActionResult Details(int id) {

            var movie = _contact.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {

                return HttpNotFound();
            }
            return View(movie);
        }
    }

}