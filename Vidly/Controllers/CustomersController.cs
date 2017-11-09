using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel()
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer) {
            /*
             * MVC framework will automatically map the requested data to this input object 
             * This is what we call Model Binding
             * If we change the type of parameter to Customer, entity frameowrk is smart enough to bind this object to form data
             * Because all the keys in the form data are prefixed
             */
            _context.Customers.Add(customer); /*write in memory, not in DB*/

            /* our DbContext has a change tracking mechanism,
             * so any time you add an object, modify the object or remove one of the existing objects
             * it will mark them as either modified or deleted
            */

            _context.SaveChanges(); /* persist the changes*/
            /* At this point our context goes through all modified objects and based on the kind of of modification,
             * it will generate SQL statements at runtime and then run them on database.
             * All these statements are wrapped in a transaction.
             * So the changes get peristed together or nothing get persisted
            */

            return RedirectToAction("Index", "Customers");

        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        [Route("Customers/Detail/{id}")]
        public ActionResult Detail(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            else
                return View(customer);

        }
    }
}