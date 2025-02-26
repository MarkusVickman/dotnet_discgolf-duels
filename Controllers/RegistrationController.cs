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
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Registration
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

            var applicationDbContext = _context.Registrations
            .Include(r => r.Competition)
            .Include(r => r.PublicUser)
            .Where(c => c.PublicUserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Competition)
                .Include(r => r.PublicUser)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registration/Create
        public async Task<IActionResult> Create(int id)
        {
            string Id = _userManager.GetUserId(User);
            var thisPublicUser = await _context.PublicUsers.FirstOrDefaultAsync(p => p.Id == Id);

            if (thisPublicUser == null)
            {
                // Hantera fallet där PublicUser inte hittas
                // Om PublicUser inte hittas, gör en redirect till PublicUser/Create
                return RedirectToAction("Create", "PublicUser");
            }

            var Competition = await _context.Competitions.FirstOrDefaultAsync(p => p.CompetitionId == id);
            
            ViewBag.Competition = Competition;
            ViewBag.PublicUserId = thisPublicUser.PublicUserId;
            ViewBag.RegisterDate = DateTime.Now;

            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationId,CompetitionId,PublicUserId,RegisterDate")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId", registration.CompetitionId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", registration.PublicUserId);
            return View(registration);
        }

        // GET: Registration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId", registration.CompetitionId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", registration.PublicUserId);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationId,CompetitionId,PublicUserId,RegisterDate")] Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
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
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionId", registration.CompetitionId);
            ViewData["PublicUserId"] = new SelectList(_context.PublicUsers, "PublicUserId", "DisplayName", registration.PublicUserId);
            return View(registration);
        }

        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Competition)
                .Include(r => r.PublicUser)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}
