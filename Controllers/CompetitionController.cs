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
    public class CompetitionController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CompetitionController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Competition
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

            int userId = thisPublicUser.PublicUserId;

            var applicationDbContext = _context.Competitions
            .Include(c => c.Course)
            .Include(c => c.PublicUser)
            .Where(c => c.PublicUserId == userId)
            .OrderByDescending(p => p.CompetitionDate);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Competition
        public async Task<IActionResult> ListAll()
        {
            var applicationDbContext = _context.Competitions
            .Include(c => c.Course)
            .Include(c => c.PublicUser)
            .OrderByDescending(p => p.CompetitionDate);

            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Competition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Registrations = await _context.Registrations
            .Include(r => r.Competition)
            .Include(r => r.PublicUser)
            .Where(r => r.CompetitionId == id)
            .ToListAsync();

            var plays = await _context.Plays
            .FirstOrDefaultAsync(r => r.CompetitionId == id);

            if (plays != null)
            {

                var playings = await _context.Playing
                .Include(p => p.Play)
                .Include(p => p.PublicUser)
                .Where(r => r.PlayId == plays.PlayId)
                .ToListAsync();

                // Sätt playings i ViewBag
                ViewBag.Playings = playings;

            }

            var competition = await _context.Competitions
                .Include(c => c.Course)
                .Include(c => c.PublicUser)
                .FirstOrDefaultAsync(m => m.CompetitionId == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // GET: Competition/Details/5
        public async Task<IActionResult> PublicDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Registations = await _context.Registrations
            .Include(r => r.Competition)
            .Include(r => r.PublicUser)
            .Where(r => r.CompetitionId == id)
            .ToListAsync();

            ViewBag.Registrations = await _context.Registrations
         .Include(r => r.Competition)
         .Include(r => r.PublicUser)
         .Where(r => r.CompetitionId == id)
         .ToListAsync();

            var plays = await _context.Plays
            .FirstOrDefaultAsync(r => r.CompetitionId == id);

            if (plays != null)
            {

                var playings = await _context.Playing
                .Include(p => p.Play)
                .Include(p => p.PublicUser)
                .Where(r => r.PlayId == plays.PlayId)
                .ToListAsync();



                // Sätt playings i ViewBag
                ViewBag.Playings = playings;

            }

            var competition = await _context.Competitions
                .Include(c => c.Course)
                .Include(c => c.PublicUser)
                .FirstOrDefaultAsync(m => m.CompetitionId == id);


            if (competition == null)
            {
                return NotFound();
            }


            return View(competition);

        }

        // GET: Competition/Create
        public async Task<IActionResult> Create()
        {
            string Id = _userManager.GetUserId(User);
            var thisPublicUser = await _context.PublicUsers.FirstOrDefaultAsync(p => p.Id == Id);


            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewBag.PublicUserId = thisPublicUser.PublicUserId;
            return View();
        }

        // POST: Competition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionId,CompetitionName,CourseId,CompetitionDate,MaxPlayerCount,PublicUserId")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", competition.CourseId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", competition.PublicUserId);
            return View(competition);
        }

        // GET: Competition/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", competition.CourseId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", competition.PublicUserId);
            return View(competition);
        }

        // POST: Competition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetitionId,CompetitionName,CourseId,CompetitionDate,MaxPlayerCount,PublicUserId")] Competition competition)
        {
            if (id != competition.CompetitionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.CompetitionId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", competition.CourseId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", competition.PublicUserId);
            return View(competition);
        }

        // GET: Competition/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions
                .Include(c => c.Course)
                .Include(c => c.PublicUser)
                .FirstOrDefaultAsync(m => m.CompetitionId == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition != null)
            {
                _context.Competitions.Remove(competition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.CompetitionId == id);
        }
    }
}
