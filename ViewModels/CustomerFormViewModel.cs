using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    //ViewModels like this are used to pass data from Controller to View.
    public class CustomerFormViewModel
    {
        //Can use List or IEnumerable data types here. Since all we need is a way to iterate over the Membership Types,
        //We can get away with using IEnumerable here instead of List, which has functions that we dont need.
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        //The below allows us to still gain access to the Customer.Name, Customer.Birthday, etc...
        //From within the View when we set the model as ViewModels.CustomerFormViewModel.
        //Without this, we would not be able to get the customer data from the database
        //And put it into the View without using the model Vidly.Models.Customer at the top of the View file.
        public Customer Customer { get; set; }
        //The IsNew property is used for the Controller's New action to notify the view that the page title should display reflecting that we are adding a new customer versus
        //Editing one that already exists since the Edit and New actions both use the same form.
        public bool IsNew { get; set; }
    }
}