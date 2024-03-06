using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP_Project.Controllers;
public class ProfileController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly DataContext _dbContext;
    public ProfileController(DataContext DbContext, UserManager<AppUser> userManager)
    {
        _dbContext = DbContext;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult GetUserByUsername()
    {
        return View();
    }

    public async Task<IActionResult> Users()
    {
        var users = await _dbContext.Users.ToListAsync();
        
        return View(users);
    }

    public async Task<IActionResult> Oneuser()
    {
        //  var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
        var users = await _userManager.FindByEmailAsync("gg@gmail.com");
        return View(users);
    }
}
