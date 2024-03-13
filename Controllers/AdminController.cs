using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.ViewModel;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;



namespace ASP_Project.Controllers;
//[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _usermanager;
    public AdminController(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _usermanager = userManager;
    }

    public IActionResult Index()
    {
        ViewBag.Enterprise = _context.CinemaEntities.ToList();
        ViewBag.Place = _context.PlaceEntities.ToList();
        return View();
    }

    public async Task<IActionResult> list()
    {
        var gets = await _context.MovieEntities.Include("CinemaEntity").ToListAsync();
        return View(gets);
    }

    public async Task<IActionResult> test()
    {
        var gets = await _context.MovieEntities.ToListAsync();
        return View(gets);
    }

    [HttpPost]
    public async Task<IActionResult> Addmovie(AddmovieVM model)
    {
        MovieEntity movie = new()
        {
            Title = model.Title,
            Description = model.Description,
        };
        var result = await _context.MovieEntities.AddAsync(movie);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("test", "admin");
        }
        return RedirectToAction("Index", "admin");
    }

    public IActionResult Addlocate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Addlocate(AddlocationVM model)
    {
        PlaceEntity place = new()
        {
            County = model.county,
            Canton = model.canton
        };
        var result = await _context.PlaceEntities.AddAsync(place);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Addlocate", "admin");
        }
        return RedirectToAction("Addlocate", "admin");
    }

    public IActionResult Addcinema()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Addcinema(AddcinemaVM model)
    {
        CinemaEntity cinema = new()
        {
            Enterprise = model.Enterprise
        };
        var result = await _context.CinemaEntities.AddAsync(cinema);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Addcinema", "admin");
        }
        return RedirectToAction("Addcinema", "admin");
    }

    public IActionResult Addprogram()
    {
        ViewBag.movie = _context.MovieEntities.ToList();
        ViewBag.place = _context.PlaceEntities.ToList();
        ViewBag.cinema = _context.CinemaEntities.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Addprogram(AddprogramVM model)
    {

        Console.WriteLine(model.Canton);
        DateTime dateTime = model.Showtime.ToUniversalTime();
        ProgramMovieEntity program = new()
        {
            MovieId = model.Titlemovie,
            PlaceId = model.Canton,
            CinemaId = model.Cinema,
            Showtime = dateTime
            // Showtime = dateTime
            // PlaceId = 
        };
        var result = await _context.ProgramMovieEntities.AddAsync(program);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "admin");
        }
        return RedirectToAction("index", "admin");
    }

    [HttpPost]
    public async Task<IActionResult> Delmovie(int movieID)
    {
        var movie = _context.MovieEntities.Where(p => p.Id == movieID).FirstOrDefault();
        if (movie != null)
        {

            var programs = await _context.ProgramMovieEntities.Where(p => p.MovieId == movieID).ToListAsync();
            var favors = await _context.FavoriteEntities.Where(f => f.MovieId == movieID).ToListAsync();
            foreach (var program in programs)
            {
                var chats = await _context.ChatEntities.Where(c => c.ProgramMovieEntityId == program.Id).ToListAsync();
                foreach (var chat in chats)
                {

                    var chatRecords = await _context.ChatRecordEntities.Where(cr => cr.ChatId == chat.Id).ToListAsync();

                    foreach (var chatrecord in chatRecords)
                    {
                        var messages = await _context.MessageRecordEntities.Where(m => m.ChatRecordEntity.Id == chatrecord.Id).ToListAsync();
                        var request = await _context.RequestEntities.Where(r => r.ChatRecordId == chatrecord.Id).ToListAsync();

                        _context.RequestEntities.RemoveRange(request);
                        _context.MessageRecordEntities.RemoveRange(messages);
                    }

                    _context.ChatRecordEntities.RemoveRange(chatRecords);
                }

                _context.ChatEntities.RemoveRange(chats);
            }

            _context.FavoriteEntities.RemoveRange(favors);
            _context.ProgramMovieEntities.RemoveRange(programs);
            _context.MovieEntities.Remove(movie);


            await _context.SaveChangesAsync();
        }
        return RedirectToAction("index", "home");
    }


    [HttpPost]
    public async Task<IActionResult> Deluser(string userID)
    {
        var user = await _usermanager.FindByIdAsync(userID);
        if (user != null)
        {
            var result = await _usermanager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "No Found User");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Loadcanton(string county)
    {
        var cantons = _context.PlaceEntities.Where(p => p.County == county).Select(p => new { p.Id, p.Canton }).Distinct().ToList();
        Console.WriteLine(cantons);
        return Json(cantons);
    }


}