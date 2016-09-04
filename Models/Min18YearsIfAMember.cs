using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    //This is a custom validation class applied in the Customer.cs file and CustomerController
    //Dont forget to include system.componentmodel.dataannotations up above
    //DOES NOT WORK FOR CLIENT-SIDE VALIDATION in MVC
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if(customer.Birthday == null)
                return new ValidationResult("Birthday is required.");
            var age = DateTime.Today.Year - customer.Birthday.Value.Year;

            return (age >= 18) ? ValidationResult.Success :
            new ValidationResult("Customer needs to be at least 18 to go on a membership.");
        }
    }
}