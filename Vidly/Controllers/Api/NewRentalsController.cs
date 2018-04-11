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

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Invalid Customer ID.");

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                var rental = new Rental { Customer = customer, Movie = movie, DateRented = DateTime.Now };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }

        
    }
}
