using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using discgolf_duels.Data;
using discgolf_duels.Models;
using Microsoft.AspNetCore.Identity;

namespace discgolf_duels.Controllers
{
    public class PlayingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PlayingController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Playing
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

            int userId = thisPublicUser.PublicUserId;

            //Inkluderar data från play, course samt publicuser till vyn
            var applicationDbContext = _context.Playing
            .Include(p => p.Play!)
            .ThenInclude(p => p.Course)
            .Include(p => p.Play!)
            .ThenInclude(p => p.Competition)
            .Include(p => p.PublicUser)
            .Where(c => c.PublicUserId == userId)
            .OrderByDescending(p => p.RegisterDate);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Playing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Inkluderar data från play, competition, course samt publicuser till vyn
            var playing = await _context.Playing
                .Include(p => p.Play!)
                .ThenInclude(p => p.Course)
            .Include(p => p.Play!)
            .ThenInclude(p => p.Competition)
                .Include(p => p.PublicUser)
                .FirstOrDefaultAsync(m => m.PlayingId == id);
            if (playing == null)
            {
                return NotFound();
            }

            return View(playing);
        }

        // GET: Playing/Create
        public async Task<IActionResult> Create(int id)
        {
            if (id != 0)
            {
                string? Id = _userManager.GetUserId(User);
                var thisPublicUser = await _context.PublicUsers.FirstOrDefaultAsync(p => p.Id == Id);

                if (thisPublicUser == null)
                {
                    // Hantera fallet där PublicUser inte hittas
                    // Om PublicUser inte hittas, gör en redirect till PublicUser/Create
                    return RedirectToAction("Create", "PublicUser");
                }

                ViewBag.PublicUserId = thisPublicUser.PublicUserId;
                ViewBag.PlayId = id;

            }
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId");
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName");
            return View();
        }

        // POST: Playing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayingId,PlayId,Par,GroupNr,PublicUserId")] Playing playing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId", playing.PlayId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", playing.PublicUserId);
            return View(playing);
        }

        // GET: Playing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Läser in och kontrollerar att id finns
            var playing = await _context.Playing
            .Include(p => p.PublicUser)
            .FirstOrDefaultAsync(p => p.PlayingId == id);

            if (playing == null)
            {
                return NotFound();
            }

            //Hämtar aktuell "spelmall" och skickar med värden tilö vyn
            var play = await _context.Plays
                .Include(p => p.Competition)
                .Include(p => p.Course)
                .Where(c => c.PlayId == playing.PlayId)
                .FirstOrDefaultAsync();

            if (play != null)
            {
                // ViewBag.PublicUserId = playing.PublicUserId;
                // ViewBag.PlayId = playing.PlayId;
                ViewBag.CompetitionName = play.Competition?.CompetitionName;
                ViewBag.CourseName = play.Course!.CourseName;
                ViewBag.CoursePar = play.Course.Par;
                ViewBag.CompetitionId = play.CompetitionId;
            }

            return View(playing);
        }

        // POST: Playing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayingId,PlayId,Par,GroupNr,PublicUserId")] Playing playing)
        {
            if (id != playing.PlayingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayingExists(playing.PlayingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("edit", "Playing", new { id = playing.PlayingId });
            }
            
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId", playing.PlayId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", playing.PublicUserId);
            return View(playing);
        }

        // GET: Playing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playing = await _context.Playing
                .Include(p => p.Play)
                .Include(p => p.PublicUser)
                .FirstOrDefaultAsync(m => m.PlayingId == id);
            if (playing == null)
            {
                return NotFound();
            }

            return View(playing);
        }

        // POST: Playing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playing = await _context.Playing.FindAsync(id);
            
            if (playing != null)
            {
                _context.Playing.Remove(playing);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayingExists(int id)
        {
            return _context.Playing.Any(e => e.PlayingId == id);
        }
    }
}
