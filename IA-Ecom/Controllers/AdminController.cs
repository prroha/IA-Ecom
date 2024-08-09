using System.Collections;
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

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                Product product = ProductMapper.MapToModel(productModel);
                product.EntryDate = DateTime.Now;
                await productService.AddProductAsync(product, productModel.ImagesInput);
                return RedirectToAction(nameof(ManageProducts));
            }
            return View(productModel);
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            if (ModelState.IsValid)
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    notificationService.AddNotification("Product Not Found", NotificationType.Error);
                    return RedirectToAction(nameof(ManageProducts));
                }
                await productService.UpdateProductAsync(product);
                notificationService.AddNotification("Product Update", NotificationType.Success);
                return RedirectToAction(nameof(ManageProducts));
            }
            notificationService.AddNotification("Could not Update Product", NotificationType.Error);
            return RedirectToAction(nameof(ManageProducts));
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
                var product = await productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    notificationService.AddNotification("Product Not Found", NotificationType.Error);
                    return RedirectToAction(nameof(ManageProducts));
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
            return RedirectToAction("ManageOrders");
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
        public async Task<IActionResult> DeleteOrder(int id)
        {
                var order = await orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    notificationService.AddNotification("Order Not Found", NotificationType.Error);
                    return RedirectToAction(nameof(ManageOrders));
                }
                await orderService.DeleteOrderAsync(id);
                notificationService.AddNotification("Order Deleted", NotificationType.Success);
                return RedirectToAction(nameof(ManageOrders));
        }

        public async Task<IActionResult> ManageUsers()
        {
            IEnumerable<User> users = await userService.GetAllUsersAsync();
            var userTasks = users.Select(async user =>
            {
                var roles = await userManager.GetRolesAsync(user);
                return new UserViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = roles.FirstOrDefault() 
                };
            });

            // Await all tasks and convert to a list
            var userViewModels = await Task.WhenAll(userTasks);        
            ManageUserViewModel viewModel = new();
            viewModel.Users = userViewModels.ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await userService.GetUserByUserIdAsync(id);
            if (user == null)
            {
                notificationService.AddNotification("User Details Not Found", NotificationType.Error);
            return RedirectToAction("ManageUsers");
            }
            var users = await userService.GetAllUsersAsync();
            List<UserViewModel> userViewModels = users.Select(o => UserMapper.MapToViewModel(o)).ToList();
            ManageUserViewModel viewModel = new ManageUserViewModel();
             viewModel.Users = userViewModels;
            viewModel.User = UserMapper.MapToViewModel(user);
            return View("ManageUsers", viewModel);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
                var user = await userService.GetUserByUserIdAsync(id);
                if (user == null)
                {
                    notificationService.AddNotification("User Not Found", NotificationType.Error);
                    return RedirectToAction(nameof(ManageUsers));
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
                notificationService.AddNotification("Feedbacl Details Not Found", NotificationType.Error);
            return RedirectToAction("ManageFeedbacks");
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
                    return RedirectToAction(nameof(ManageFeedbacks));
                }
                await feedbackService.DeleteFeedbackAsync(id);
                notificationService.AddNotification("Feedback Deleted", NotificationType.Success);
                return RedirectToAction(nameof(ManageFeedbacks));
        }
    }
}
