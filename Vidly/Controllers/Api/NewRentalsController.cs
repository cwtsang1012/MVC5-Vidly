using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
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

        //Optimistic approach for private API
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            //if customer table can't match newRentalDto.CustomerId, it will throw exception.
            var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.Availability == 0)
                    return BadRequest("Movie is not available.");

                movie.Availability--;
                var rental = new Rental { Customer = customer, Movie = movie, DateRented = DateTime.Now };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }


        //Defensive approach - with lots of validation checks and explicit error messages for public API 
        /*
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            if (newRentalDto.MovieIds.Count == 0)
                return BadRequest("No Movie Ids have been given.");

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Invalid Customer ID.");

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRentalDto.MovieIds.Count)
                return BadRequest("One or more Movie Ids are invalid.");

            foreach (var movie in movies)
            {
                if (movie.Availability == 0)
                    return BadRequest("Movie is not available.");

                movie.Availability--;
                var rental = new Rental { Customer = customer, Movie = movie, DateRented = DateTime.Now };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
        */
        
    }
}
