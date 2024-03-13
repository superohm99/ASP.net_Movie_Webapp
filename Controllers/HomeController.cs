using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;
using ASP_Project.Data;
using Microsoft.AspNetCore.Authorization;

namespace ASP_Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly DataContext _context;
    // private readonly UserManager<AppUser> _userManager;
    public HomeController(DataContext context,ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public IActionResult Index()
    {
        ViewBag.movie = _context.MovieEntities.ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Showmovie()
    {
        ViewBag.movie = _context.MovieEntities.ToList();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
