using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using discgolf_duels.Data;
using discgolf_duels.Models;
using Microsoft.AspNetCore.Identity;

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
            string Id = _userManager.GetUserId(User);
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

        // GET: Play/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var play = await _context.Plays
                .Include(p => p.Competition)
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.PlayId == id);
            if (play == null)
            {
                return NotFound();
            }

            return View(play);
        }

        // GET: Play/Create Competition
        public async Task<IActionResult> Create(int id)
        {
            if (id != 0)
            {
                var competition = await _context.Competitions.FindAsync(id);

                var thisRegistrations = await _context.Registrations
                .Include(r => r.Competition)
                .Include(r => r.PublicUser)
                .Where(r => r.CompetitionId == id)
                .ToListAsync();

                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == competition.CourseId);

                Play play = new Play
                {
                    CompetitionId = id,
                    CourseId = course.CourseId
                };

                _context.Plays.Add(play);
                await _context.SaveChangesAsync();

                var thisPlay = _context.Plays.FirstOrDefault(c => c.CompetitionId == id);

                foreach (var registration in thisRegistrations)
                {

                    Playing playing = new Playing
                    {
                        PlayId = thisPlay.PlayId,
                        Par = course?.Par,
                        GroupNr = null,
                        PublicUserId = registration.PublicUserId
                    };

                    _context.Playing.Add(playing);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("index", "Playing");
            }
            return View();

        }

        // GET: Play/Create
        public async Task<IActionResult> CreateSingle(int playId)
        {
            if (playId != 0)
            {

                string Id = _userManager.GetUserId(User);
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

                    var thisPlaying = await _context.Playing
                    .Where(p => p.PublicUserId == thisPublicUser.PublicUserId)
                    .OrderByDescending(p => p.RegisterDate)
                    .FirstOrDefaultAsync();
                    return RedirectToAction("Edit", "Playing", new { id = thisPlaying!.PlayingId });

                }


            }
            return RedirectToAction("index", "Home");

        }

        // POST: Play/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayId,CompetitionId,CourseId,Active")] Play play)
        {
            if (ModelState.IsValid)
            {
                _context.Add(play);
                await _context.SaveChangesAsync();

                var findPlay = await _context.Plays.FirstOrDefaultAsync(p => p.CourseId == play.CourseId && p.CompetitionId == null);

                var course = await _context.Courses.FirstOrDefaultAsync(p => p.CourseId == play.CourseId);

                string Id = _userManager.GetUserId(User);
                var thisPublicUser = await _context.PublicUsers.FirstOrDefaultAsync(p => p.Id == Id);

                Playing playing = new Playing
                {
                    PlayId = findPlay.PlayId,
                    Par = course?.Par,
                    GroupNr = null,
                    PublicUserId = thisPublicUser.PublicUserId
                };

                _context.Playing.Add(playing);
                await _context.SaveChangesAsync();

                return RedirectToAction("index", "Playing");
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId", play.CompetitionId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", play.CourseId);
            return View(play);
        }

        // GET: Play/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var play = await _context.Plays.FindAsync(id);
            if (play == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId", play.CompetitionId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", play.CourseId);
            return View(play);
        }

        // POST: Play/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayId,CompetitionId,CourseId,Active")] Play play)
        {
            if (id != play.PlayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(play);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayExists(play.PlayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId", play.CompetitionId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", play.CourseId);
            return View(play);
        }

        // GET: Play/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var play = await _context.Plays
                .Include(p => p.Competition)
                .Include(p => p.Course)
                .FirstOrDefaultAsync(m => m.PlayId == id);
            if (play == null)
            {
                return NotFound();
            }

            return View(play);
        }

        // POST: Play/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var play = await _context.Plays.FindAsync(id);
            if (play != null)
            {
                _context.Plays.Remove(play);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayExists(int id)
        {
            return _context.Plays.Any(e => e.PlayId == id);
        }
    }
}
