using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.ViewModels
{
    public class FeedbackViewModel
    {
        public int FeedbackId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }

        // Additional fields as needed for feedback details
    }
}