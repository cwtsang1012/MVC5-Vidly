using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the customer's name.")]
        [StringLength(255, ErrorMessage = "Length must be equal or less than 255 characters.")]
        public string Name { get; set; }

        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
       
        public byte MembershipTypeId { get; set; } /* EF recognise this as foreign key by convention */

        public MembershipTypeDto MembershipType { get; set; }
    }
}