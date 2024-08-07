using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using Microsoft.AspNetCore.Authorization;

namespace IA_Ecom.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("ADMIN"))
        {
                return RedirectToAction("Dashboard", "Admin");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}