using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;

namespace IA_Ecom.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: /Feedback
        public IActionResult Index()
        {
            var feedbacks = _feedbackService.GetAllFeedbacksAsync();
            return View(feedbacks);
        }

        // GET: /Feedback/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _feedbackService.AddFeedbackAsync(feedback);
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        // Other actions for managing feedback
    }
}