using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //GET /api/Customers/id
        public Customer GetCustomer(int id)
        {
            var customer =  _context.Customers.SingleOrDefault(c => c.Id == id);

            // this is part of RESTful convention
            // If the given resource is not found, return the standard NotFound HTTP response
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        // By convention, when we create a resource, we return a newly created resource to the client
        //POST /api/Customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // By convention, return resource type or void is both ok.
        // 2 parameters: id - from URL & customer - from request body
        //PUT /api/Customers/id
        [HttpPut]
        public void EditCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            _context.SaveChanges();
        }


        //DELETE /api/Customers/id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
