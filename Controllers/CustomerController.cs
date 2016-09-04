using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //The below action will accept /Customers/New even though the view isn't actually called New anymore
        //Due to the routing. See how it now sends the user to CustomerForm just like Edit does too.
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            //The IsNew is to make sure the View displays the page title properly, see CustomerForm.cshtml if statement
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes,
                IsNew = true
            };
            
            return View("CustomerForm", viewModel);
        }
        //Actions that modify data should NEVER be accessible by HTTPGet. So we only allow HttpPost here.
        //Also the CustomerFormViewModel parameter causes the MVC to bind the data to the CustomerFormViewModel.
        //This means that the form's submitted data will go to viewModel.Customer.WhateverpropertyHere and viewModel.MembershipTypes.PropertyHere.
        //This allows our data, via HTTP POST, to get placed into the data models and then into the database.
        //Do a debug breakpoint on this action and inspect the viewModel to see evidence.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                //This method of data entry allows you to explicitly control which items in the Db can be updated by the user. More secured than an update-all method like TryUpdateModel()
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.Name = customer.Name;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //We have to specify the view name here because it is different from this Action method's name.
            //This is also why we have to separately define the viewModel and pass it as a second argument.
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            //Without the Include method below, the MembershipType would be null when the page loads, causing an error.
            
            
            return View();
        }

        public ActionResult Details(int id)
        {
            //The below line is using a lambda expression to get the total list of customers from GetCustomers() and then return the customer associated with the request id number.
            //The lambda expression is saying, "For each customer in List<Customer>, return the one whose Id is equal to id.
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

       
    }
}