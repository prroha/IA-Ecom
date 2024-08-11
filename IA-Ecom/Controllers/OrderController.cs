using System.Security.Claims;
using IA_Ecom.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.RequestModels;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Controllers
{
        [Authorize] 
    public class OrderController(IOrderService orderService,IPaymentService paymentService, IUserService userService, INotificationService notificationService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetCartDetails()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await orderService.GetCartDetailsAsync(customerId);

            if (cart == null)
            {
                notificationService.AddNotification("Cart is Empty", NotificationType.Validation);
                return Redirect(Request.Headers["Referer"].ToString());
            }

            OrderViewModel viewModel = OrderMapper.MapToViewModel(cart);
            return View("CartDetails", viewModel);
        }
        
        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> AddToCartAsync(OrderItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID from identity

                // Retrieve the user from the database
                var user = await userService.GetUserByUserIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                // Check if the user's address is missing
                bool isAddressMissing = string.IsNullOrEmpty(user.Address);
                if (isAddressMissing)
                {
                    notificationService.AddNotification("Please add your address information from the profile page.", NotificationType.Validation);
                    
                    return RedirectToAction("Details", "Product", new { id = viewModel.ProductId });
                }                
                OrderItem orderItem = OrderMapper.MapToModel(viewModel);
                orderItem.CustomerId = userId;
                await orderService.AddToCartAsync(orderItem, user);
                // return RedirectToAction(nameof(Index));
                notificationService.AddNotification("Item Successfully added to Cart. Visit Cart Details to Checkout", NotificationType.Success);
                return RedirectToAction("Details", "Product", new { id = viewModel.ProductId });
            }
                notificationService.AddNotification("Couldnot Perform the Action at the Moment.", NotificationType.Error);
            return RedirectToAction("Details", "Product", new { id = viewModel.ProductId });
        }
        
        private async Task<IActionResult> Checkout(PaymentMethod paymentMethod)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if user ID is not found
            }
            var success = await orderService.CheckoutAsync(userId, paymentMethod);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(OrderConfirmation));
        }
        
        [HttpGet]
        public IActionResult GetPaymentDetails()
        {
            return View("Checkout");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(PaymentMethod paymentMethod)
        {
            if (!PaymentMethodValidator.ValidatePaymentMethod(paymentMethod))
            {
                notificationService.AddNotification("Invalid payment details.", NotificationType.Validation);
                return Redirect(Request.Headers["Referer"].ToString());
            }

            notificationService.AddNotification("Payment Details Validated", NotificationType.Success);
            return await Checkout(paymentMethod);
        }

        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (orderId == 0)
            {
                var orderConfirmation = new OrderConfirmationViewModel();
                orderConfirmation.OrderItems = new List<OrderItemViewModel>();
                return View(orderConfirmation);
            }
            var order = await orderService.GetOrderByIdAsync(orderId);
            var payment = await paymentService.GetPaymentByOrderId(orderId);

            if (payment == null)
            {
                return NotFound();
            }

            OrderConfirmationViewModel viewModel = OrderMapper.MapToViewModel(payment, order);
            return View(viewModel);
        }

    }
}