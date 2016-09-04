using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    //Don't forget to include the project models namespace up top!
    public class CustomersApiController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersApiController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/customersapi
        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            //Select the customer based on the provided id; match the id to database id.
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //If the customer id can't be found, customer will be null and trigger the following exception
            //which is a REST standard convetion.
            if(customer == null)
                NotFound();
            //If the customer is NOT null, then we return the customer info.
            //Don't forget to change return type of this action to IHttpActionResult or else
            //This will throw exceptions.
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        [HttpPost]
        //Action will ONLY happen during POST requests. You can also use the PostCustomer action instead of this attribute.
        //IHTTPActionResult is similar to the regular ActionResult in the regular Controller. However, it allows us to use special helper methods
        //Such as BadRequest() used below. It also allows us to return code 201 - Created to the client when they run a POST request.
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) //Create a NEW Customer called customer
        {
            if (!ModelState.IsValid)
            {
                BadRequest(); //Another REST convention here

            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto );
        }
        
        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto) //Gets the customer from the URL (id) and request body (Customer) like the CreateCustomer method 
        {
            //If the provided information is null or not valid, throw the below Bad Request exception.
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); //Another REST convention here

            }
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //You can get the AutoMapper tool to do this for you like so:
            //Because the object csustomerInDb already exists, it can be passed to the Mapper as a second
            //arguement to map to. The other times, we didn't yet have another object so we mapped it to a new object with
            //var = . See above.
            //Resharper grays out the parameters to the Generic below because they can be inferred. You can remove them if you like.

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            

            _context.SaveChanges();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
