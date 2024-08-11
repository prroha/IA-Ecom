namespace IA_Ecom.Models
{
    public class Feedback: BaseModel
    {
        public int FeedbackId => Id;
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}