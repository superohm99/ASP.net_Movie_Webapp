using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.ViewModel;
using Microsoft.EntityFrameworkCore;
using ASP_Project.Data;
using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Controllers;
public class ChatController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly DataContext _context;
    public ChatController(DataContext context,UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        ViewBag.movie = _context.MovieEntities.ToList();
        ViewBag.place = _context.PlaceEntities.ToList();
        ViewBag.program =_context.ProgramMovieEntities.Include("MovieEntity").Include("CinemaEntity").Include("PlaceEntity").ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddchatVM model)
    {

        DateTime date = DateTime.UtcNow; 
        var showtime = _context.ProgramMovieEntities.Where(p => p.Id == model.Showtime).Select(p => p.Showtime).FirstOrDefault();
        TimeSpan duration_time =showtime - date;
        ChatEntity chat = new()
        {
            startAt = date,
            endAt = showtime,
            ProgramMovieEntityId = model.Showtime,
            duration = duration_time
        };

        var result = await _context.ChatEntities.AddAsync(chat);
        if (result != null)
        {
            await _context.SaveChangesAsync();
        }

        ChatRecordEntity chatrecord = new()
        {
            Status = true,
            ChatId = chat.Id,
            AppUserId = _userManager.GetUserId(HttpContext.User)
        };

        var result_record = await _context.ChatRecordEntities.AddAsync(chatrecord);
        if (result_record != null)
        {
            // Console.WriteLine(dateTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("create", "Chat");
        }
        return RedirectToAction("Index","Chat");
    }

    [HttpGet]
    public IActionResult Loadmovie(int movieId)
    {
        // Console.WriteLine(movieId);
        var programs = _context.ProgramMovieEntities.Where(p => p.MovieId == movieId).Select(p => p.Id).ToList();

        var result = new List<object>();
        var seenEnterprises = new HashSet<string>();
        foreach(var program in programs)
        {
        var cinemaId = _context.ProgramMovieEntities.Where(p => p.Id == program).Select(p => p.CinemaId).FirstOrDefault();
    
        var enterprise_result = _context.CinemaEntities.Where(c => c.Id == cinemaId).Select(c => new{c.Id,c.Enterprise}).FirstOrDefault();


        if (enterprise_result != null && seenEnterprises.Add(enterprise_result.Enterprise))
        {
            result.Add(new { enterprise_result });
        }
        // result.Add(new {enterprise_result});
        }

        // var enterprise = _
        // Console.WriteLine(result);
        return Json(result);
    }

    [HttpGet]
    public IActionResult Loadcinema(int cinemaId, int movieId)
    {
        // Console.WriteLine(cinemaId);
        var programs = _context.ProgramMovieEntities.Where(p => p.CinemaId == cinemaId && p.MovieId == movieId).Select(p => p.Id).ToList();

        var result = new List<object>();
        var seenEnterprises = new HashSet<string>();
        foreach(var program in programs)
        {
        var placeId = _context.ProgramMovieEntities.Where(p => p.Id == program).Select(p => p.PlaceId).FirstOrDefault();
    
        var place_result = _context.PlaceEntities.Where(c => c.Id == placeId).Select(c => new{c.Id,c.County,c.Canton}).FirstOrDefault();
    
        if (place_result != null && seenEnterprises.Add(place_result.County))
        {
        result.Add(new {place_result});
        }

        }

        // var enterprise = _
        // Console.WriteLine(result);
        return Json(result);
    }

    [HttpGet]
    public IActionResult Loadcanton(int movieId,int cinemaId,string placestr)
    {
        var programs = _context.ProgramMovieEntities.Where(p => p.CinemaId == cinemaId && p.MovieId == movieId).Select(p => p.Id).ToList();

        var result = new List<object>();
        var seenEnterprises = new HashSet<string>();
        
        foreach (var program in programs)
        {
       
            var place = _context.ProgramMovieEntities.Where(p => p.Id == program).Select(p => p.PlaceId).FirstOrDefault();
            
            var place_result = _context.PlaceEntities.Where(c => c.Id == place).Select(c => new{c.Id,c.Canton}).FirstOrDefault();
            if (place_result != null && seenEnterprises.Add(place_result.Canton))
            {
            result.Add(new {place_result});
            }
        }

        // var places = _context.PlaceEntities.Where(p => p.County == placeId).Select(p => new {p.Id, p.Canton}).ToList();
        return Json(result);
    }

    [HttpGet]
    public IActionResult Loadshowtime(int movieId, int cinemaId, int placeId)
    {
        Console.WriteLine("movie"+movieId);
        Console.WriteLine("cinema"+cinemaId);
        Console.WriteLine("place"+placeId);
        var programs = _context.ProgramMovieEntities.Where(p => p.MovieId == movieId && p.CinemaId == cinemaId && p.PlaceId == placeId).Select(p => new{p.Id, p.Showtime}).ToList();
        return Json(programs);
    }
}
