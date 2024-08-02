namespace IA_Ecom.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}