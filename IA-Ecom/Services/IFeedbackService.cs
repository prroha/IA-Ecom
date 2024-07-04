using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;

namespace IA_Ecom.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task AddFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(int id);
    }
}