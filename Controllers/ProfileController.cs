using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;
using ASP_Project.ViewModel;

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
        var userid = _userManager.GetUserId(HttpContext.User);
        if (userid == null)
        {
            return RedirectToAction("login","account");
        }
        else
        {
        AppUser user = _userManager.FindByIdAsync(userid).Result;
        return View(user);
        }
    }

    public IActionResult Edit()
    {
        var userid = _userManager.GetUserId(HttpContext.User);
        if (userid == null)
        {
            return RedirectToAction("login","account");
        }
        ViewBag.username = _userManager.GetUserName(User);
        ViewBag.user = User;
        AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
        ViewBag.facebook = user.Facebook;
        ViewBag.ig = user.IG;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditprofileVM model)
    {
        var userid = _userManager.GetUserId(HttpContext.User);
        AppUser user = await _userManager.FindByIdAsync(userid);
        if (model.Name != null)
        {
            user.Name = model.Name;
        }
        if (model.IG != null)
        {
            user.IG = model.IG;
        }
        if (model.Facebook != null)
        {
            user.Facebook = model.Facebook;
        }
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
        // Redirect to a success page or return a success message
        return RedirectToAction("Index", "profile");
        }
        return RedirectToAction("edit","profile");
    }

    // [HttpPost]
    // public async Task<IActionResult> Edit()
    // {
    //     IdentityResult x = await _userManager.UpdateAsync()
    // }



    // public IActionResult GetUserByUsername()
    // {
    //     return View();
    // }

    // public async Task<IActionResult> Users()
    // {
    //     var users = await _dbContext.Users.ToListAsync();
        
    //     return View(users);
    // }

    // public async Task<IActionResult> Oneuser()
    // {
    //     //  var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
    //     var users = await _userManager.FindByEmailAsync("gg@gmail.com");
    //     return View(users);
    // }
}
