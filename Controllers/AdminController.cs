using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.ViewModel;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;



namespace ASP_Project.Controllers;
// [Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;
    public AdminController(DataContext context,UserManager<AppUser> userManager)
    {
          _context = context;
          _userManager = userManager;
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

    public IActionResult AddMovie()
    {
        ViewBag.Enterprise = _context.CinemaEntities.ToList();
        ViewBag.Place = _context.PlaceEntities.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddMovie(AddmovieVM model)
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
            return RedirectToAction("AddMovie", "admin");  //ถ้าสำเร็จจะ redirect ไปที่ /admin/test
        }
        return RedirectToAction("movies", "admin");
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
        // IndoChina Time
        dateTime = dateTime.AddHours(7);
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
            // admin/EditMovie/:id
            // get id from programMovieEntity
            var movie = await _context.MovieEntities.FindAsync(model.Titlemovie);
            if (movie != null)
            {
                return RedirectToAction("EditMovie", "admin", new { id = movie.Id });
            }
            return RedirectToAction("movies", "admin");
        }
        return RedirectToAction("movies", "admin");
    }

    [HttpGet]
    public IActionResult Loadcanton(string county)
    {
        var cantons = _context.PlaceEntities.Where(p => p.County == county).Select(p => new { p.Id, p.Canton}).Distinct().ToList();
        Console.WriteLine(cantons);
        return Json(cantons);
    }

    //GEt movies 
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

 public async Task<IActionResult> Reports()
    {
        var gets = await _context.ReportEntities.ToListAsync();

        var users = new List<AppUser>();
        var dates = new List<DateTime>();
        var times = new List<DateTime>();
        foreach (var item in gets)
        {
            var user = await _userManager.FindByIdAsync(item.AppUserId);
            // split gets[i].Sendtime into date and time
            var date = item.Sendtime.ToString("dd/MM/yyyy");
            var time = item.Sendtime.ToString("HH:mm");
            // create new object to store date and time
            users.Add(user);
        }
        ViewBag.Users = users;
        ViewBag.Dates = dates;
        ViewBag.Times = times;

        return View(gets);
    }

    public async Task<IActionResult> Movies()
    {
        var gets = await _context.MovieEntities.ToListAsync();
        return View(gets);
    }

    public async Task<IActionResult> EditMovie(int? id)
    {
        if (id == 0 || id == null)
        {
            return RedirectToAction("movies", "admin");
        }

        var movie = await _context.MovieEntities.FindAsync(id);

        if (movie != null)
        {
            return View(movie);
        }

        return RedirectToAction("movies", "admin");
    }

[HttpPost]
    public async Task<IActionResult> EditMovie(MovieEntity model)
    {
        var movie = await _context.MovieEntities.FindAsync(model.Id);

        if (movie.Title != model.Title && model.Title != null)
        {
            movie.Title = model.Title;
        }

        if (movie.Description != model.Description && model.Description != null)
        {
            movie.Description = model.Description;
        }

        if (movie.Image != model.Image && model.Image != null)
        {
            movie.Image = model.Image;
        }

        if (movie != null)
        {
            _context.MovieEntities.Update(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("movies", "admin");
        }

        return RedirectToAction("movies", "admin");

    }

    [HttpGet]
    public async Task<IActionResult> AllPlaces(int? id)
    {
        if (id == 0 || id == null)
        {
            return RedirectToAction("movies", "admin");
        }
        var gets = await _context.ProgramMovieEntities.Where(p => p.MovieId == id).Select(p => p.Showtime.Date).ToListAsync();
        return Json(gets);
    }

    [HttpGet]
    public async Task<IActionResult> GetPlaces(int? id, DateTime date)
    {

        if (id == 0 || id == null)
        {
            return RedirectToAction("movies", "admin");
        }

        var placesIDs = await _context.ProgramMovieEntities.Where(p => p.MovieId == id && p.Showtime.Date == date.Date).Select(p => p.PlaceId).Distinct().ToListAsync();
        var res = new List<object>();
        foreach (var placeid in placesIDs)
        {

            var places = await _context.PlaceEntities.Where(p => p.Id == placeid).Select(p => new { p.Id, p.Canton, p.County }).FirstOrDefaultAsync();

            var showtimes = await _context.ProgramMovieEntities
                                .Where(p => p.MovieId == id && p.PlaceId == placeid && p.Showtime.Date == date.Date)
                                .Select(p => p.Showtime.TimeOfDay.ToString(@"hh\:mm"))
                                .Distinct()
                                .ToListAsync();

            if (places != null)
            {
                res.Add(new { id = places.Id, canton = places.Canton, county = places.County, showtimes = showtimes });
            }
        }
        return Json(res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteShowtime(int? id)
    {
        if (id == 0 || id == null)
        {
            return RedirectToAction("movies", "admin");
        }

        var showtime = await _context.ProgramMovieEntities.FindAsync(id);

        if (showtime != null)
        {
            _context.ProgramMovieEntities.Remove(showtime);
            await _context.SaveChangesAsync();
            return RedirectToAction("movies", "admin");
        }

        return RedirectToAction("movies", "admin");
    }

    public async Task<IActionResult> GetProgramID(int? movieid, int? placeid, DateTime date)
    {
        if (movieid == 0 || movieid == null || placeid == 0 || placeid == null)
        {
            return RedirectToAction("movies", "admin");
        }

        var programObj = await _context.ProgramMovieEntities.Where(p => p.MovieId == movieid && p.PlaceId == placeid && p.Showtime.Date == date.Date).ToListAsync();

        if (programObj != null)
        {
            return Json(programObj);
        }
        return RedirectToAction("movies", "admin");
    }

    public async Task<IActionResult> GetPlaceID(int? movieid, string canton, string county)
    {
        if (movieid == 0 || movieid == null || canton == null || county == null)
        {
            return RedirectToAction("movies", "admin");
        }

        var placeObj = await _context.PlaceEntities.Where(p => p.Canton == canton && p.County == county).ToListAsync();

        if (placeObj != null)
        {
            return Json(placeObj);
        }
        return RedirectToAction("movies", "admin");
    }



}
