using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesApiController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesApiController()
        {
            _context = new ApplicationDbContext();
            
        }
        //GET api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            var moviesDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MoviesDto>);

            return Ok(moviesDtos);
        }

        //Get api/movies/1

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                NotFound();
            }
            return Ok(Mapper.Map<Movie, MoviesDto>(movie));
        }

        //POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MoviesDto moviesDto)
        {
            //Validates the data sent by the client.
            if (!ModelState.IsValid)
            {
                BadRequest(); //Another REST convention here

            }
            //The below Generic literally turns MoviesDto into a movie object that can have its data put into the DB.
            var movie = Mapper.Map<MoviesDto,Movie>(moviesDto); //Map the data from the Dto (entry point from client) to the domain model this time.

            _context.Movies.Add(movie);
            _context.SaveChanges();
            moviesDto.Id = movie.Id;
            //Creates a 201 CREATED message with the URL and received content?
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), moviesDto);
        }

        //PUT /api/movies/1
        [HttpPut]
        //id tells it which movie to pick from the database and Dto accepts the data to be updated.
        public IHttpActionResult UpdateMovie(int id, MoviesDto moviesDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); //Another REST convention here

            }

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            //Make sure the movie exists in the DB by the ID provided.
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //Maps the accepted data from the client to the domain model and then commits the domain model's changes into the database.
            Mapper.Map<MoviesDto, Movie>(moviesDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            //Selects the movie by id
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null) //If there is not matching id in the database, throw exception.
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }


}
