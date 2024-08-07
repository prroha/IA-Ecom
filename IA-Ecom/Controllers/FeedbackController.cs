using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;

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

// GET: /ContactUs
        public IActionResult ContactUs()
        {
            return View("Home/ContactUs", new ContactUsViewModel());
        }

        // POST: /ContactUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the form submission, e.g., save to database or send an email
                // Redirect to a confirmation page or display a success message
                return RedirectToAction("Confirmation");
            }

            return View("Home/ContactUs", new ContactUsViewModel());
        }

        public IActionResult Confirmation()
        {
            return View();
        }    }
}