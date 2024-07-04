using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IA_Ecom.Models
{
    public class User: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }

        // Additional properties as needed
        public string Address { get; set; }
        public string PhoneNumber { get; set; }    
        // Constructor for initialization if needed
         public User() { }
    }
}