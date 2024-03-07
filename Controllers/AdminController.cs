using Microsoft.AspNetCore.Mvc;
using ASP_Project.Models;
using ASP_Project.ViewModel;
using ASP_Project.Data;
using Microsoft.EntityFrameworkCore;

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
        return View();
    }

    public async Task<IActionResult> test()
    {
        var gets = await _context.MovieEntities.ToListAsync();
        return View(gets);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AddmovieVM model)
    {
        int cinema;
        if (model.Cinema == "Major")
        {
            cinema = 2;
        }
        else
        {
            cinema = 1;
        }
        MovieEntity movie = new ()
        {
            Title = model.Title,
            Description = model.Description, 
            Showtime = model.Showtime,
            CinemaId = cinema
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

}
