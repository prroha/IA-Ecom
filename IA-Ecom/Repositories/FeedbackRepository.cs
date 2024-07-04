using IA_Ecom.Data;
using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implement additional methods specific to Feedback if any
    }
}