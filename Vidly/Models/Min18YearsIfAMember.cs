using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Dtos;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = new Customer();
            if (validationContext.ObjectType.Name == "CustomerDto")
            {
                var customerDto  = (CustomerDto)validationContext.ObjectInstance;
                
            }
            else
                customer = (Customer)validationContext.ObjectInstance; /*give access to containing class, ie Customer)*/
            /* Since this is an object, we need to cast it to the containing class */
            if (customer.MembershipTypeId == MembershipType.Unknown 
                || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required.");

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old to go on the membership.");
        }
    }
}