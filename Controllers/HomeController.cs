using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using discgolf_duels.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using discgolf_duels.Data;
using Microsoft.EntityFrameworkCore;

namespace discgolf_duels.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Playing
           .Include(p => p.Play)
           .ThenInclude(p => p!.Course)
           .Include(p => p.PublicUser)
           .OrderByDescending(p => p.RegisterDate);
           
        ViewData["PlayId"] = new SelectList(_context.Plays.Include(c => c.Course).Where(c => c.CompetitionId == null || c.CompetitionId == 0), "PlayId", "Course.CourseName");
        return View(await applicationDbContext.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
