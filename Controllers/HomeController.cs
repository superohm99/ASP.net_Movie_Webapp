using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // private readonly UserManager<AppUser> _userManager;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
