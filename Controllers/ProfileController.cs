using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;
using ASP_Project.ViewModel;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;


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

    public IActionResult Requests()
    {
        var result = new List<object>();
        var  chatrecords = _dbContext.ChatRecordEntities.Where(p => p.AppUserId == _userManager.GetUserId(HttpContext.User)).Select(c => c.Id).ToList();
        
        foreach (var chat in chatrecords)
        {
            var req = _dbContext.RequestEntities.Where(p => p.ChatRecordId == chat).FirstOrDefault();
            
            if (req != null)
            {
                var user_select = _userManager.FindByIdAsync(req.AppUserId).Result;
                result.Add(new {req, user_select});
            }
        }

        ViewBag.Result = result;
        return View();
    }


    public IActionResult Submitquest()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Requests(int ohmreqIdreal,int ohmreqId, string ohmuserId)
    {
            if (ohmuserId == null)
            {
                Console.WriteLine("sdfsd5fsd");
                return RedirectToAction("privary", "home");
            }
            else
            {
                Console.WriteLine("chatrecordid "+ohmreqId);
                Console.WriteLine("userid "+ohmuserId);
                var chatid = await _dbContext.ChatRecordEntities.Where(p => p.Id == ohmreqId).Select(c => c.ChatId).FirstOrDefaultAsync();
                if (chatid == null)
                {
                    Console.WriteLine("xzv,mzn");
                }
                ChatRecordEntity chatrecord = new()
                {
                    Status = false,
                    ChatId = chatid,
                    AppUserId = ohmuserId
                };
                Console.WriteLine("asfafafa :" + chatid);
                var result_record = await _dbContext.ChatRecordEntities.AddAsync(chatrecord);
                if (result_record != null)
                {
                    var chat = await _dbContext.ChatEntities.Where(p => p.Id == chatid).FirstOrDefaultAsync();
                    if (chat != null)
                    {
                        chat.remainNumber++;
                        _dbContext.Update(chat);
                        var req = await _dbContext.RequestEntities.Where(p => p.Id == ohmreqIdreal).FirstOrDefaultAsync();
                        _dbContext.RequestEntities.Remove(req);
                        await _dbContext.SaveChangesAsync();
                        return RedirectToAction("index", "profile");
                    }
                }
            
            return RedirectToAction("index", "home");
            }
    }

    public IActionResult LikeMovie()
    {
        var favors = _dbContext.FavoriteEntities.Where(p => p.AppUserId ==  _userManager.GetUserId(HttpContext.User)).ToList();
        var res = new List<object>();
        foreach (var favor in favors)
        {
            var movie = _dbContext.MovieEntities.Where(p => p.Id == favor.MovieId).Select(c => new {c.Id,c.Image,c.Title}).FirstOrDefault();
            res.Add(new {movie});
        }
        ViewBag.likemovie = res;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LikeMovie(int movieid)
    {
        if (_dbContext.FavoriteEntities.Any(p => p.MovieId == movieid && p.AppUserId ==  _userManager.GetUserId(HttpContext.User)))
        {
            return RedirectToAction("Showmovie", "home");;
        }
        FavoriteEntity favor = new()
        {
            AppUserId = _userManager.GetUserId(HttpContext.User),
            MovieId = movieid
        };
        var result = await _dbContext.FavoriteEntities.AddAsync(favor);
        if (result != null)
        {
            // Console.WriteLine(dateTime);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("LikeMovie", "profile");
        }
        return View();
    }

    public async Task<IActionResult> Dellike(int movieId)
    {
        var favor = await _dbContext.FavoriteEntities.Where(p => p.MovieId == movieId && p.AppUserId == _userManager.GetUserId(HttpContext.User)).FirstOrDefaultAsync();
        if (favor != null)
        {
            _dbContext.FavoriteEntities.Remove(favor);
            _dbContext.SaveChanges();
            Console.WriteLine("Success Delete");
            return RedirectToAction("LikeMovie", "profile");
        }

        return RedirectToAction("LikeMovie", "profile");
    }

    public IActionResult Group()
    {
        var chatrecordsID = _dbContext.ChatRecordEntities.Where(p => p.AppUserId ==  _userManager.GetUserId(HttpContext.User)).ToList();
        var res = new List<object>();
        Console.WriteLine("asfkhaskfa5");
        foreach (var chatrecordId in chatrecordsID)
        {
                Console.WriteLine("asfkhaskfa4" + chatrecordId.Id);
                var hostrecordId = _dbContext.ChatRecordEntities.Where(p => p.ChatId == chatrecordId.ChatId && p.Status == true).FirstOrDefaultAsync().Result;
                Console.WriteLine("asfkhaskfa3" + hostrecordId.Id);
                if (hostrecordId != null)
                {
                var chatId =  hostrecordId.ChatId;
                var chat =  _dbContext.ChatEntities.Where(p => p.Id == chatId).FirstOrDefault();
                if (chat != null)
                {
                    Console.WriteLine("asfkhaskfa2");
                var programId = chat.ProgramMovieEntityId;
                var movieId = _dbContext.ProgramMovieEntities.Where(p => p.Id == programId).Select(c => c.MovieId).FirstOrDefault();
                if (movieId != null)
                {
                Console.WriteLine("asfkhaskfa" + movieId);
                var movie = _dbContext.MovieEntities.Where(p => p.Id == movieId).FirstOrDefaultAsync().Result;
                var hostuserId = hostrecordId.AppUserId;
                var hostuser = _userManager.FindByIdAsync(hostuserId).Result;
                var hostuserimage = hostuser.Image;
                var movietitle = movie.Title;
                var movieimage = movie.Image;
                res.Add(new {hostuserimage,hostuser.Name,movieimage,movietitle,chatId});
                }
                }
                }
                
        }
        // // foreach (var chatrecord in chatrecords)
        // // {
        // //     var movie = _dbContext.ChatEntities.Where(p => p.Id == chatrecord).Select(c => new {c.Id,c.ProgramMovieEntityId,c.}).FirstOrDefault();
        // //     res.Add(new {movie});
        // // }
        ViewBag.groups = res;   
        
        return View();
    }


    

    // [HttpPost]
    // public async Task<IActionResult> Group()
    // {
    //     var chatrecords = _dbContext.ChatRecordEntities.Where(p => p.AppUserId ==  _userManager.GetUserId(HttpContext.User)).Select(c => c.ChatId).ToList();
    //     var chatrecordsID = _dbContext.ChatRecordEntities.Where(p => p.AppUserId ==  _userManager.GetUserId(HttpContext.User)).Select(c => c.Id).ToList();
    //     var res = new List<object>();
    //     foreach (var chatrecordId in chatrecordsID)
    //     {
    //             var hostrecordId = _dbContext.ChatRecordEntities.Where(p => p.Id == chatrecordId && p.Status == true).FirstOrDefaultAsync().Result;
    //             if (hostrecordId != null)
    //             {
    //             var chatId =  hostrecordId.ChatId;
    //             var chat =  _dbContext.ChatEntities.Where(p => p.Id == chatId).FirstOrDefaultAsync().Result;
    //             if (chat != null)
    //             {
    //             var programId = chat.ProgramMovieEntityId;
    //             var movieId = _dbContext.ProgramMovieEntities.Where(p => p.Id == programId).Select(c => c.MovieId).FirstOrDefault();
    //             if (movieId != null)
    //             {
    //             var movie = _dbContext.MovieEntities.Where(p => p.Id == movieId).FirstOrDefaultAsync().Result;
    //             var hostuserId = hostrecordId.AppUserId;
    //             var hostuser = _userManager.FindByIdAsync(hostuserId).Result;
    //             var hostuserimage = hostuser.Image;
    //             res.Add(new {hostuserimage,hostuser.Name,movie.Image,movie.Title});
    //             }
    //             }
    //             }
                
    //     }
    //     // foreach (var chatrecord in chatrecords)
    //     // {
    //     //     var movie = _dbContext.ChatEntities.Where(p => p.Id == chatrecord).Select(c => new {c.Id,c.ProgramMovieEntityId,c.}).FirstOrDefault();
    //     //     res.Add(new {movie});
    //     // }
    //     ViewBag.groups = res;   
    //     return View();
    // }


}