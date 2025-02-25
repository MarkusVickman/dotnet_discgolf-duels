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
    public class PlayingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Playing
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Playing.Include(p => p.Play).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Playing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playing = await _context.Playing
                .Include(p => p.Play)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PlayingId == id);
            if (playing == null)
            {
                return NotFound();
            }

            return View(playing);
        }

        // GET: Playing/Create
        public IActionResult Create()
        {
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Playing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayingId,PlayId,Par,GroupNr,UserId")] Playing playing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId", playing.PlayId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playing.UserId);
            return View(playing);
        }

        // GET: Playing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playing = await _context.Playing.FindAsync(id);
            if (playing == null)
            {
                return NotFound();
            }
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId", playing.PlayId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playing.UserId);
            return View(playing);
        }

        // POST: Playing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayingId,PlayId,Par,GroupNr,UserId")] Playing playing)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayId"] = new SelectList(_context.Plays, "PlayId", "PlayId", playing.PlayId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", playing.UserId);
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
                .Include(p => p.User)
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
