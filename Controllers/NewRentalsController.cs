using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{

    
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
            {
                return BadRequest("No movies have been entered.");
            }
            //Single used instead of SingleOrDefault because this is an internal-use API only. That way an if-null block isn't needed.
            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if (customer == null)
            {
                return BadRequest("CustomerId not valid.");
            }

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
            {
                return BadRequest("One or more movies are invalid.");
            }
            foreach (var movie in movies)
            {
                if (movie.NumberAvailable <= 0)
                {
                    return BadRequest(movie.Name + " is not available!");
                }
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }

        //public IHttpActionResult GetRentals()
        //{
        //    return _context.Rentals.ToList();
        //}
    }
}
