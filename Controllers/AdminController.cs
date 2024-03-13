using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.ViewModel;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;



namespace ASP_Project.Controllers;
public class AdminController : Controller
{
    private readonly DataContext _context;
    public AdminController(DataContext context)
    {
          _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.Enterprise =_context.CinemaEntities.ToList();
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
        MovieEntity movie = new ()
        {
            Title = model.Title,
            Description = model.Description,
            Image = model.Image, 
        };
        var result = await _context.MovieEntities.AddAsync(movie);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("test", "admin");  //ถ้าสำเร็จจะ redirect ไปที่ /admin/test
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
        PlaceEntity place = new ()
        {
            County = model.county ,
            Canton = model.canton
        };
        var result = await _context.PlaceEntities.AddAsync(place);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Addlocate", "admin");
        }
        return RedirectToAction("Addlocate","admin");
    }

    public IActionResult Addcinema()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Addcinema(AddcinemaVM model)
    {
        CinemaEntity cinema = new ()
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
        return RedirectToAction("Addcinema","admin");
    }

    public IActionResult Addprogram()
    {
        ViewBag.movie =_context.MovieEntities.ToList();
        ViewBag.place = _context.PlaceEntities.ToList();
        ViewBag.cinema = _context.CinemaEntities.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Addprogram(AddprogramVM model)
    {

        Console.WriteLine(model.Canton);
        DateTime dateTime = model.Showtime.ToUniversalTime();
        ProgramMovieEntity program = new ()
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
            return RedirectToAction("Addprogram", "admin");
        }
        return RedirectToAction("Addprogram","admin");
    }

    [HttpGet]
    public IActionResult Loadcanton(string county)
    {
        var cantons = _context.PlaceEntities.Where(p => p.County == county).Select(p => new { p.Id, p.Canton}).Distinct().ToList();
        Console.WriteLine(cantons);
        return Json(cantons);
    }


     [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        var movie = await _context.MovieEntities.ToListAsync();

        if (movie != null)
        {
            return Json(movie);
        }

        return RedirectToAction("movies", "admin");
    }



}
