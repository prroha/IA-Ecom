using System.Security.Claims;
using IA_Ecom.Mappers;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Controllers
{
    public class FeedbackController(IFeedbackService feedbackService, INotificationService notificationService) : Controller
    {
        public IActionResult Index()
        {
            var feedbacks = feedbackService.GetAllFeedbacksAsync();
            return View(feedbacks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbackService.AddFeedbackAsync(feedback);
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if user ID is not found
            }
            if (ModelState.IsValid)
            {
                Feedback feedback = FeedbackMapper.MapToModel(model);
                feedback.Date = DateTime.Now;
                feedback.UserId = userId;
                feedbackService.AddFeedbackAsync(feedback);
                notificationService.AddNotification("Your Message was submitted. Admin will review it and get back to you soon.", NotificationType.Success);
                return RedirectToAction("ContactUs", "Home");
            }
            notificationService.AddNotification("Could not Save. Please Try Again Later", NotificationType.Validation);
            return View("../Home/ContactUs", new ContactUsViewModel());
        }

        public IActionResult Confirmation()
        {
            return View();
        }    }
}