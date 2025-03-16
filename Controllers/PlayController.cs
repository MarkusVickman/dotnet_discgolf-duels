using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using discgolf_duels.Data;
using discgolf_duels.Models;
using Microsoft.AspNetCore.Identity;


/*
play controllerns vyer används inte i applicationen utan här sker allt skapande av tabeller i bakgrunden/ i denna kontroller
*/

namespace discgolf_duels.Controllers
{
    public class PlayController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PlayController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Play
        public async Task<IActionResult> Index()
        {
            string? Id = _userManager.GetUserId(User);
            var thisPublicUser = await _context.PublicUsers.FirstOrDefaultAsync(p => p.Id == Id);

            if (thisPublicUser == null)
            {
                // Hantera fallet där PublicUser inte hittas
                // Om PublicUser inte hittas, gör en redirect till PublicUser/Create
                return RedirectToAction("Create", "PublicUser");
            }


            var applicationDbContext = _context.Plays.Include(p => p.Competition).Include(p => p.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Play/Create Competition
        //När en tävling startas initieras denna route som skapar nya playingtabeller(rundor) för varje registrerad spelare i tävlingen.
        public async Task<IActionResult> Create(int id)
        {
            if (id != 0)
            {

                //Kontrollerar om tävlingen redan är startad i så fall går den inte att starta om.
                var competition = await _context.Competitions.FindAsync(id);
                if (competition != null)
                {
                    var hasCompStarted = await _context.Plays.AnyAsync(r => r.CompetitionId == competition.CompetitionId);

                    return RedirectToAction("Details", "competition", new { id = id });
                }

                //Hämtar alla registrerade spelare till tävlingen
                var thisRegistrations = await _context.Registrations
                .Include(r => r.Competition)
                .Include(r => r.PublicUser)
                .Where(r => r.CompetitionId == id)
                .ToListAsync();

                //Läser in tävlingen bana
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == competition!.CourseId);

                string par = "";

                // För tävlingen skapas en spelmall (play)
                if (course != null)
                {
                    Play play = new Play
                    {
                        CompetitionId = id,
                        CourseId = course.CourseId
                    };

                    //Varje hål/korg startar på 0 kast
                    for (int i = 0; i < course.Par.Length; i++)
                    {
                        par = par + "0";
                    }

                    _context.Plays.Add(play);
                    await _context.SaveChangesAsync();
                }

                var thisPlay = _context.Plays.FirstOrDefault(c => c.CompetitionId == id);

                // För varje spelare i tävlingen skapas ett spel (playing) enligt tävlingens spelmall (play)
                if (thisPlay != null)
                {
                    foreach (var registration in thisRegistrations)
                    {

                        Playing playing = new Playing
                        {
                            PlayId = thisPlay.PlayId,
                            Par = par,
                            GroupNr = null,
                            PublicUserId = registration.PublicUserId
                        };

                        _context.Playing.Add(playing);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction("index", "Playing");
            }
            return View();
        }

        // GET: Play/Create
        //Vid skapande av enskilt spel
        //En färdig spelmall skickas med som parameter
        public async Task<IActionResult> CreateSingle(int playId)
        {
            if (playId != 0)
            {

                string? Id = _userManager.GetUserId(User);
                var thisPublicUser = await _context.PublicUsers.FirstOrDefaultAsync(p => p.Id == Id);

                if (thisPublicUser == null)
                {
                    // Hantera fallet där PublicUser inte hittas
                    // Om PublicUser inte hittas, gör en redirect till PublicUser/Create
                    return RedirectToAction("Create", "PublicUser");
                }

                var play = await _context.Plays.FirstOrDefaultAsync(c => c.PlayId == playId && (c.CompetitionId == null || c.CompetitionId == 0));

                if (play != null)
                {

                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == play.CourseId);

                    string par = "";

                    if (course != null)
                    {
                        //Varje hål/korg startar på 0 kast
                        for (int i = 0; i < course.Par.Length; i++)
                        {
                            par = par + "0";
                        }
                    }

                    Playing playing = new Playing
                    {
                        PlayId = play.PlayId,
                        Par = par,
                        GroupNr = null,
                        PublicUserId = thisPublicUser.PublicUserId
                    };

                    _context.Playing.Add(playing);
                    await _context.SaveChangesAsync();

                    //Användaren dirigeras till den nyskapade rundan
                    var thisPlaying = await _context.Playing
                    .Where(p => p.PublicUserId == thisPublicUser.PublicUserId)
                    .OrderByDescending(p => p.RegisterDate)
                    .FirstOrDefaultAsync();
                    return RedirectToAction("Edit", "Playing", new { id = thisPlaying!.PlayingId });
                }
            }
            return RedirectToAction("index", "Home");
        }
    }
}
