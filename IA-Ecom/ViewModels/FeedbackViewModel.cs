using System.ComponentModel.DataAnnotations;
using IA_Ecom.Models;

namespace IA_Ecom.ViewModels
{
    public class FeedbackViewModel
    {
        public int FeedbackId { get; set; }

        public string Username { get; set; }
        public string UserFullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public int Rating { get; set; }
        public User User { get; set; }
    }
}