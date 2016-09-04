using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        
       // [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipTypeDto MembershipType { get; set; }    
        //Implicitly required because it is a byte
        public byte MembershipTypeId { get; set; } //Foreign Key
        public int Id { get; set; }
        //Makes the Name field required (not null) and caps its max length at 255 characters.
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}