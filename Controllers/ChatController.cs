using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.ViewModel;
using Microsoft.EntityFrameworkCore;
using ASP_Project.Data;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text;

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

    public IActionResult Join()
    {
        ViewBag.movie =_context.MovieEntities.ToList();
        return View();
    }

public async Task<IActionResult> getMovies()
    {
        var movies = await _context.MovieEntities.ToListAsync();
        return Json(movies);
    }

    public async Task<IActionResult> getMovieByID(int? id)
    {
        var movie = await _context.MovieEntities.Where(p => p.Id == id).ToListAsync();
        return Json(movie);
    }

    public async Task<IActionResult> SelectedMovie(int? id)
    {
        var programMovieEntities = await _context.ProgramMovieEntities.Where(p => p.MovieId == id).ToListAsync();
        ViewBag.programMovieEntities = programMovieEntities;
        return View();
    }
    
    public IActionResult Filterjoin()
    {
        return View();
    }
    


    [HttpPost]
    public async Task<IActionResult> Filterjoin(FilterchatVM model)
    {
        var programs = await _context.ProgramMovieEntities.Where(p => p.MovieId == model.movieid && p.CinemaId == model.cinemaid && p.PlaceId == model.placeid && p.Showtime == model.showtime).Select(p => p.Id).ToListAsync();
        
        var result = new List<object>();
        foreach (var program in programs)
        {
            var chats = await _context.ChatEntities
        .Where(p => p.ProgramMovieEntityId == program)
        .Select(c => new { c.Id, c.remainNumber, c.maxNumber })
        .Distinct()
        .ToListAsync();

        foreach (var chat in chats)
        {
                var userid = await _context.ChatRecordEntities
                    .Where(p => p.ChatId == chat.Id)
                    .Select(c => c.AppUserId)
                    .FirstOrDefaultAsync();

                var chatrecordid = await _context.ChatRecordEntities
                    .Where(p => p.ChatId == chat.Id && p.Status == true)
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();

                AppUser user = await _userManager.FindByIdAsync(userid);
                var image_user = user.Image;

                result.Add(new { chat, image_user, chatrecordid });
                }
            
            // var chat = await _context.ChatEntities.Where(p => p.ProgramMovieEntityId == program).Select(c => new {c.Id,c.remainNumber,c.maxNumber}).FirstOrDefaultAsync();
            // if (chat != null)
            // {
            //     var userid = await _context.ChatRecordEntities.Where(p => p.ChatId == chat.Id).Select(c => c.AppUserId).FirstOrDefaultAsync();
            //     var chatrecordid = await _context.ChatRecordEntities.Where(p => p.ChatId == chat.Id && p.Status == true).Select(c => c.Id).FirstOrDefaultAsync();
            //     AppUser user = await _userManager.FindByIdAsync(userid);
            //     var image_user = user.Image;
            //     // var image_user = await _context.
            //     result.Add(new { chat,image_user,chatrecordid });
            // }
            
        }
            // ส่งค่า result ไปที่ View ชื่อ "Filter" โดยส่งผ่าน ViewBag
        ViewBag.Result = result;
    
         return View("Filterjoin");
    }

    [HttpPost]
    public async Task<IActionResult> Joinchat(JoinchatVM model)
    {

        if (_userManager.GetUserId(HttpContext.User) == null)
        {
            return RedirectToAction("login","account");
        }
        if (_context.ChatRecordEntities.Any(p => p.ChatId == model.chatid && p.AppUserId == _userManager.GetUserId(HttpContext.User)))
        {
            return RedirectToAction("join","chat");
        }
        RequestEntity request = new()
        {
            Status = false,
            AppUserId = _userManager.GetUserId(HttpContext.User),
            ChatRecordId = model.chatrecordid
        };
        var result_record = await _context.RequestEntities.AddAsync(request);
        await _context.SaveChangesAsync();
        return RedirectToAction("index", "profile");
        // ChatRecordEntity chatrecord = new()
        // {
        //     Status = false,
        //     ChatId = model.chatid,
        //     AppUserId = _userManager.GetUserId(HttpContext.User)
        // };
        // var result_record = await _context.ChatRecordEntities.AddAsync(chatrecord);
        // if (result_record != null)
        // {
        //     var chat = _context.ChatEntities.Where(p => p.Id == model.chatid).FirstOrDefault();
        //     if (chat != null)
        //     {
        //     chat.remainNumber++;
        //     _context.Update(chat);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction("index", "profile");
        //     }
        // }


        // return View("join","chat");
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
            duration = duration_time,
            maxNumber = model.MaxNumber
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
            return RedirectToAction("index", "profile");
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