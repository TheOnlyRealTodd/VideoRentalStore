using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        //The below is a NAVIGATION property, it allows you to navigate from Customer to its membership type.
        public MembershipType MembershipType { get; set; }
        [Display(Name = "Membership Type")]
        //Implicitly required because it is a byte
        public byte MembershipTypeId { get; set; } //Foreign Key
        public int Id { get; set; }
        //Makes the Name field required (not null) and caps its max length at 255 characters.
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}