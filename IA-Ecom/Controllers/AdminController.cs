using System.Collections;
using System.Security.Claims;
using IA_Ecom.Mappers;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace IA_Ecom.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController(
        IProductService productService,
        IOrderService orderService,
        IUserService userService,
    UserManager<User> userManager,
        INotificationService notificationService,
        IFeedbackService feedbackService)
        : Controller
    {
        public IActionResult Index()
        {
                return RedirectToAction(nameof(Dashboard));
        }
        
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Dashboard()
        {
            var usersCount = await userService.CountAllAsync();
            var productsCount = await productService.CountAllAsync();
            var ordersCount = await orderService.CountAllAsync();
            var feedbackCount = await feedbackService.CountAllAsync();

            var dashboardData = new AdminDashboardViewModel
            {
                UsersCount = usersCount,
                ProductsCount = productsCount,
                OrdersCount = ordersCount,
                FeedbacksCount = feedbackCount,
               Users = [],
               Products = [],
               Orders = [],
               Feedbacks = [],
            };

            return View("Dashboard", dashboardData);
        }
        
        [HttpPost("profile")]
        public async Task<IActionResult> Profile()
        {
            var profile = new ProfileViewModel();
            if (ModelState.IsValid)
            {
                // await _productService.AddProductAsync(product);
                // return RedirectToAction(nameof(Products));
            }
            return View(profile);
        }
        
        public async Task<IActionResult> ManageProducts()
        {
            IEnumerable<Product> products = await productService.GetAllProductsAsync();
            List<ProductViewModel> productsViewModel = products.Select(o => ProductMapper.MapToViewModel(o)).ToList();
            ManageProductViewModel viewModel = new ManageProductViewModel();
            viewModel.Products = productsViewModel;
            return View(viewModel);
        }

        [HttpGet()]
        public async Task<IActionResult> ProductDetails(int id)
        {
            IEnumerable<Product> products = await productService.GetAllProductsAsync();
            Product product = products.First(p => p.ProductId == id);
            if (product == null)
            {
                notificationService.AddNotification("Product Not Found", NotificationType.Error);
                return RedirectToAction("ManageProducts");
            }
            List<ProductViewModel> productsViewModel = products.Select(o => ProductMapper.MapToViewModel(o)).ToList();
            ManageProductViewModel viewModel = new ManageProductViewModel();
            viewModel.Products = productsViewModel;
            viewModel.Product = ProductMapper.MapToViewModel(product);
            return View("ManageProducts", viewModel);
        }

        [HttpPost()]
        public async Task<IActionResult> AddProduct(ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                if (productModel.ProductId != 0)
                {
                    await EditProduct(productModel);
                }
                Product product = ProductMapper.MapToModel(productModel);
                product.EntryDate = DateTime.UtcNow;
                await productService.AddProductAsync(product, productModel.ImagesInput);
                notificationService.AddNotification("Product Saved Successfully", NotificationType.Success);
                // return RedirectToAction(nameof(ManageProducts));
            return Redirect(Request.Headers["Referer"].ToString());
            }
            notificationService.AddNotification("Error Saving", NotificationType.Validation);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        private async Task<IActionResult> EditProduct(ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = await productService.GetProductByIdAsync(productModel.ProductId);
                if (product == null)
                {
                    notificationService.AddNotification("Product Not Found", NotificationType.Error);
                    return RedirectToAction(nameof(ManageProducts));
                }
                ProductMapper.MapToModel(product, productModel);
                product.EntryDate = DateTime.UtcNow;
                await productService.UpdateProductAsync(product, productModel.ImagesInput);
                notificationService.AddNotification("Product Updated Successfully", NotificationType.Success);
                // return RedirectToAction(nameof(ManageProducts));
            return Redirect(Request.Headers["Referer"].ToString());
            }
            notificationService.AddNotification("Could not Update Product", NotificationType.Error);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                notificationService.AddNotification("Product Not Found", NotificationType.Error);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            await productService.DeleteProductAsync(id);
            notificationService.AddNotification("Product Deleted", NotificationType.Success);
            return RedirectToAction(nameof(ManageProducts));
        }

        public async Task<IActionResult> ManageOrders()
        {
            IEnumerable<Order> orders = await orderService.GetAllOrdersAsync();
            List<OrderViewModel> orderViewModel = orders.Select(o => OrderMapper.MapToViewModel(o)).ToList();
            ManageOrderViewModel viewModel = new ManageOrderViewModel();
            viewModel.Orders = orderViewModel;
            return View(viewModel);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            IEnumerable<Order> orders = await orderService.GetAllOrdersAsync();
            Order order = orders.First(p => p.OrderId == id);
            if (order == null)
            {
                notificationService.AddNotification("Order Not Found", NotificationType.Error);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            List<OrderViewModel> orderViewModel = orders.Select(o => OrderMapper.MapToViewModel(o)).ToList();
            ManageOrderViewModel viewModel = new ManageOrderViewModel();
            viewModel.Orders = orderViewModel;
            viewModel.Order = OrderMapper.MapToViewModel(order);
            return View("ManageOrders", viewModel);
            // var order = await orderService.GetOrderByIdAsync(id);
            // if (order == null)
            // {
            //     return NotFound();
            // }
            // return View(order);
        }
        [HttpPost()]
        public async Task<IActionResult> EditOrder(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid &&  !String.IsNullOrEmpty(orderViewModel.CustomerId))
            {
                var customer = await userService.GetUserByUserIdAsync(orderViewModel.CustomerId);
                Order existingOrder = await orderService.GetOrderByIdAsync(orderViewModel.OrderId);
                if (existingOrder == null)
                {
                    notificationService.AddNotification("Order Not Found", NotificationType.Error);
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                Order order = OrderMapper.MapToModel(orderViewModel, customer);
                order.UpdatedDate = DateTime.UtcNow;
                await orderService.UpdateOrderAsync(order);
                return RedirectToAction(nameof(ManageOrders));
            }
                    notificationService.AddNotification("Error Occurred", NotificationType.Validation);
        return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                notificationService.AddNotification("Order Not Found", NotificationType.Error);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            await orderService.DeleteOrderAsync(id);
            notificationService.AddNotification("Order Deleted", NotificationType.Success);
            return RedirectToAction(nameof(ManageOrders));
        }

        public async Task<IActionResult> ManageUsers()
        {
            IEnumerable<User> users = await userService.GetAllUsersAsync();
            ManageUserViewModel viewModel = new();
            viewModel.Users = await GetUserViewModels(users);
            viewModel.User = new UserViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await userService.GetUserByUserIdAsync(id);
            if (user == null)
            {
                notificationService.AddNotification("User Details Not Found", NotificationType.Error);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            IEnumerable<User> users = await userService.GetAllUsersAsync();
            ManageUserViewModel viewModel = new();
            List<UserViewModel> userModels = await GetUserViewModels(users);
            viewModel.Users = userModels;
            viewModel.User = userModels.First(u => u.UserId == id);
            return View("ManageUsers", viewModel);
        }

        private async Task<List<UserViewModel>> GetUserViewModels(IEnumerable<User> users)
        {
            var userTasks = users.Select(async user =>
            {
                var roles = await userManager.GetRolesAsync(user);
                var userModel = UserMapper.MapToViewModel(user);
                userModel.Role = roles.FirstOrDefault();
                return userModel;
            });

            // Await all tasks and convert to a list
            return (await Task.WhenAll(userTasks)).ToList();        
            
        }

        public async Task<IActionResult> EditUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid &&  !String.IsNullOrEmpty(userViewModel.UserId))
            {
                var existingUser = await userService.GetUserByUserIdAsync(userViewModel.UserId);
                if (existingUser == null)
                {
                    notificationService.AddNotification("User Not Found", NotificationType.Error);
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                User user = UserMapper.MapToModel(userViewModel);
                user.UpdatedDate = DateTime.UtcNow;
                await userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(ManageUsers));
            }
            notificationService.AddNotification("Error Occurred", NotificationType.Validation);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userService.GetUserByUserIdAsync(id);
            if (user == null)
            {
                notificationService.AddNotification("User Not Found", NotificationType.Error);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            await userService.DeleteUserAsync(id);
            notificationService.AddNotification("User Deleted", NotificationType.Success);
            return RedirectToAction(nameof(ManageUsers));
        }
        
        public async Task<IActionResult> ManageFeedbacks()
        {
            var feedbacks = await feedbackService.GetAllFeedbacksAsync();
            List<FeedbackViewModel> feedbackViewModel = feedbacks.Select(o => FeedbackMapper.MapToViewModel(o)).ToList();
            ManageFeedbackViewModel viewModel = new ManageFeedbackViewModel();
             viewModel.Feedbacks = feedbackViewModel;
            return View(viewModel);
        }

        public async Task<IActionResult> FeedbackDetails(int id)
        {
            var feedback = await feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                notificationService.AddNotification("Feedback Details Not Found", NotificationType.Error);
        return Redirect(Request.Headers["Referer"].ToString());
            }
            var feedbacks = await feedbackService.GetAllFeedbacksAsync();
            List<FeedbackViewModel> feedbackViewModel = feedbacks.Select(o => FeedbackMapper.MapToViewModel(o)).ToList();
            ManageFeedbackViewModel viewModel = new ManageFeedbackViewModel();
             viewModel.Feedbacks = feedbackViewModel;
            viewModel.Feedback = FeedbackMapper.MapToViewModel(feedback);
            return View("ManageFeedbacks", viewModel);
        }
        
        public async Task<IActionResult> DeleteFeedback(int id)
        {
                var feedback = await feedbackService.GetFeedbackByIdAsync(id);
                if (feedback == null)
                {
                    notificationService.AddNotification("Feedback Not Found", NotificationType.Error);
        return Redirect(Request.Headers["Referer"].ToString());
                }
                await feedbackService.DeleteFeedbackAsync(id);
                notificationService.AddNotification("Feedback Deleted", NotificationType.Success);
                return RedirectToAction(nameof(ManageFeedbacks));
        }
    }
}
