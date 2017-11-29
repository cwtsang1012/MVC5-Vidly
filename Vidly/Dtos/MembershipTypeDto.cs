using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MembershipTypeDto
    {
        // There's no need to add all membership type properties here 
        // Because if a client wants to know the details about a given membership type
        // they can use the Id here to send a request to a potential new endpoint for membership type
        // keep all thing lightweight

        public byte Id { get; set; }
 
        public string Name { get; set; }
    }
}