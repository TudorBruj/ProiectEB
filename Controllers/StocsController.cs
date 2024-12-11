using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectEB.Data;
using ProiectEB.Models;

namespace ProiectEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StocsController : Controller
    {
        private readonly ProiectEBContext _context;

        public StocsController(ProiectEBContext context)
        {
            _context = context;
        }

        // GET: Stocs
        public async Task<IActionResult> Index(string searchLocation, int? minQuantity)
        {
            var stocuri = _context.Stoc
                                  .Include(s => s.Produs)
                                  .AsQueryable();

            // Filtrare după locație depozit
            if (!string.IsNullOrEmpty(searchLocation))
            {
                stocuri = stocuri.Where(s => s.LocatieDepozit.Contains(searchLocation));
            }

            // Filtrare după cantitate minimă
            if (minQuantity.HasValue)
            {
                stocuri = stocuri.Where(s => s.Cantitate >= minQuantity.Value);
            }

            return View(await stocuri.ToListAsync());
        }

        // GET: Stocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoc = await _context.Stoc
                .Include(s => s.Produs)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (stoc == null)
            {
                return NotFound();
            }

            return View(stoc);
        }

        // GET: Stocs/Create
        public IActionResult Create()
        {
            ViewData["IdProdus"] = new SelectList(_context.Produs, "Id", "Nume");
            return View();
        }

        // POST: Stocs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdProdus,LocatieDepozit,Cantitate")] Stoc stoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdProdus"] = new SelectList(_context.Produs, "Id", "Nume", stoc.IdProdus);
            return View(stoc);
        }

        // GET: Stocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoc = await _context.Stoc.FindAsync(id);
            if (stoc == null)
            {
                return NotFound();
            }

            ViewData["IdProdus"] = new SelectList(_context.Produs, "Id", "Nume", stoc.IdProdus);
            return View(stoc);
        }

        // POST: Stocs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdProdus,LocatieDepozit,Cantitate")] Stoc stoc)
        {
            if (id != stoc.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StocExists(stoc.ID))
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

            ViewData["IdProdus"] = new SelectList(_context.Produs, "Id", "Nume", stoc.IdProdus);
            return View(stoc);
        }

        // GET: Stocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoc = await _context.Stoc
                .Include(s => s.Produs)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (stoc == null)
            {
                return NotFound();
            }

            return View(stoc);
        }

        // POST: Stocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stoc = await _context.Stoc.FindAsync(id);
            if (stoc != null)
            {
                _context.Stoc.Remove(stoc);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StocExists(int id)
        {
            return _context.Stoc.Any(e => e.ID == id);
        }
    }
}
