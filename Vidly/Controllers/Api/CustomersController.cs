using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        //private IMapper mapper;  

        public CustomersController()
        {
            _context = new ApplicationDbContext();

           //configure AutoMapper to know what types you want to map from model to model DTO
           //var config = new MapperConfiguration(cfg =>
           // {
           //     cfg.CreateMap<Customer, CustomerDto>().ReverseMap();
           // });
           // mapper = config.CreateMapper();
        }

        //GET /api/Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            //return _context.Customers.ToList().Select(c => mapper.Map<Customer, CustomerDto>(c));
            var customerQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));
                
            var customerDtos = customerQuery.ToList()
                .Select(c => Mapper.Map<Customer, CustomerDto>(c));

            return Ok(customerDtos);
            /* or (static method - source and target must be defined as there are more than one mapping configuration)
             * return _context.Customers.ToList().Select(c => Mapper.Map<Customer, CustomerDto>(c));
             */
        }

        //GET /api/Customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer =  _context.Customers.SingleOrDefault(c => c.Id == id);

            // this is part of RESTful convention
            // If the given resource is not found, return the standard NotFound HTTP response
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // By convention, when we create a resource, we return a newly created resource to the client
        //POST /api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            //customer's Id is updated after saving to database => update DTO before it is returned back to the view)
            customerDto.Id = customer.Id;

            //lst argument: URI of newly created resource
            //2nd argument: Pass the actual object that was created
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // By convention, return resource type or void is both ok.
        // 2 parameters: id - from URL & customer - from request body
        //PUT /api/Customers/id
        [HttpPut]
        public void EditCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //2nd parameter is used if the passing object is existed, it will auto update object.
            //Without 2nd parmeter will return a new object
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);

            //customerInDb.Name = customer.Name;
            //customerInDb.Birthdate = customer.Birthdate;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;
            //customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

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
