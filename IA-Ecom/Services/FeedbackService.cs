using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;

namespace IA_Ecom.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<int> CountAllAsync()
        {
            return await _feedbackRepository.CountAllAsync();
        }
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _feedbackRepository.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _feedbackRepository.GetByIdAsync(id);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _feedbackRepository.AddAsync(feedback);
            await _feedbackRepository.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
            await _feedbackRepository.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback != null)
            {
                _feedbackRepository.Remove(feedback);
                await _feedbackRepository.SaveChangesAsync();
            }
        }
    }
}