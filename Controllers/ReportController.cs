using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.Data;
using ASP_Project.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Controllers;

public class ReportController : Controller
{
    private readonly DataContext _context;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public ReportController(DataContext context,SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();

    }

    public IActionResult Report()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Report(ReportVM model)
    {
        // DateTime dateTime = new DateTime();
        if (_signInManager.IsSignedIn(User))
        {
        DateTime date = DateTime.UtcNow;
        ReportEntity report = new()
        {
            Title = model.Title,
            Description = model.Description,
            Sendtime = date,
            AppUserId = _userManager.GetUserId(HttpContext.User)
        };

        Console.WriteLine("5646466544");
        var result = await _context.ReportEntities.AddAsync(report);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        }
        return View(model);
    }

}
