using IA_Ecom.Data;
using IA_Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class FeedbackRepository(ApplicationDbContext context)
        : GenericRepository<Feedback>(context), IFeedbackRepository
    {
        /*public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            return await context.Feedbacks.Include(f => f.User).ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int feedbackId)
        {
            return await context.Feedbacks.Include(f => f.User)
                .FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            context.Feedbacks.Add(feedback);
            await context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            context.Feedbacks.Update(feedback);
            await context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await context.Feedbacks.FindAsync(feedbackId);
            if (feedback != null)
            {
                context.Feedbacks.Remove(feedback);
                await context.SaveChangesAsync();
            }
        }    */
        
    }
}