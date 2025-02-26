using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using discgolf_duels.Data;
using discgolf_duels.Models;

namespace discgolf_duels.Controllers
{
    public class PlayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Play
        public async Task<IActionResult> Index()
        {
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

        // GET: Play/Create
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

                var course = _context.Courses.FirstOrDefault(c => c.CourseId == competition.CourseId);

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
                }
            }

            else
            {
                ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId");
                ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            }
            return View();
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
                return RedirectToAction(nameof(Index));
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
