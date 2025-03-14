using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using discgolf_duels.Data;
using discgolf_duels.Models;
using Microsoft.AspNetCore.Identity;

namespace discgolf_duels.Controllers
{
    public class PublicUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PublicUserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: PublicUser
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

            var applicationDbContext = _context.PublicUsers
            .Include(p => p.IdentityUser)
            .Where(c => c.PublicUserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PublicUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicUser = await _context.PublicUsers
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.PublicUserId == id);
            if (publicUser == null)
            {
                return NotFound();
            }

            return View(publicUser);
        }

        // GET: PublicUser/Create
        public IActionResult Create()
        {
            string? Id = _userManager.GetUserId(User);

            ViewBag.Id = Id;
            return View();
        }

        // POST: PublicUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicUserId,DisplayName,PDGA_Nr,PictureURL,Id")] PublicUser publicUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", publicUser.Id);
            return View(publicUser);
        }

        // GET: PublicUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicUser = await _context.PublicUsers.FindAsync(id);
            if (publicUser == null)
            {
                return NotFound();
            }

            ViewBag.Id = publicUser.Id;

            return View(publicUser);
        }

        // POST: PublicUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PublicUserId,DisplayName,PDGA_Nr,PictureURL,Id")] PublicUser publicUser)
        {
            if (publicUser.PublicUserId == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicUserExists(publicUser.PublicUserId))
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
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", publicUser.Id);
            return View(publicUser);
        }

        // GET: PublicUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicUser = await _context.PublicUsers
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.PublicUserId == id);
            if (publicUser == null)
            {
                return NotFound();
            }

            return View(publicUser);
        }

        // POST: PublicUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicUser = await _context.PublicUsers.FindAsync(id);
            if (publicUser != null)
            {
                _context.PublicUsers.Remove(publicUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicUserExists(int id)
        {
            return _context.PublicUsers.Any(e => e.PublicUserId == id);
        }
    }
}
