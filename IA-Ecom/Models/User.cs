using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IA_Ecom.Models
{
    public class User: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]  // Ensure this property is not mapped to the database
        public string FullName => $"{FirstName} {LastName}".Trim();

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }    
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}