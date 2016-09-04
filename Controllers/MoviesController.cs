using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {

                return View("List");
            }
            return View("ReadOnlyList");
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                
                Genres = genres,
                IsNew = true
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            //Select movie where the model  (database) id equals the provided id from the View's hidden id tag.
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            //Make sure that the id actually exists:
            if (movie == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //The MovieForm View is programmed to submit the form to this action of this controller at the beginning of the form.
        //It is also told in the very first line of the page which data model is being used, allowing it to map all of the form fields to that model via the lambdas.
        //It sends an object of the type of the data model specified, MovieFormViewModel, to this action.
        //Because the MovieFormViewModel has properties which map (directly correspond) to Movie,
        //and because MovieFormViewModel accepts a movie in its constructor, the action accepts the form data
        //As type Movie and then injects the values into the database and saves the changes.
        //If the form is not valid, the if statement simply displays the form again with
        //whatever data was entered by the user, and then also displays the validation messages.
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    
                    IsNew = false,
                    //This Genre prop is initialized here because we must populate it with what is in the database.
                    //We could not do that from the MovieFormViewModel without the dbcontext being in there, plus
                    //the form actually uses the Genre id property of the Movie class to set the Genre anyway.
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                //DateAdded is not a nullable property/database column so it must be set. Since it is not set by the form, it is
                //set here.
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                //This method of data entry allows you to explicitly control which items in the Db can be updated by the user. More secured than an update-all method like TryUpdateModel()
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
                _context.SaveChanges();

            
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if(movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Redirect to the Edit form
                return RedirectToAction("Edit", new {id = id});
            }
        }



        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
    }
}