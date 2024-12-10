using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectEB.Data;
using ProiectEB.Models;

namespace ProiectEB.Controllers
{
    public class ProdusController : Controller
    {
        private readonly ProiectEBContext _context;

        public ProdusController(ProiectEBContext context)
        {
            _context = context;
        }

        // GET: Produs
        public async Task<IActionResult> Index(string searchName, decimal? minPrice, decimal? maxPrice, int? minQuantity)
        {
            // Query-ul pentru produse incluzând Recenzii și Stoc
            var produse = _context.Produs
                                  .Include(p => p.Recenzii)
                                  .Include(p => p.Stoc)
                                  .AsQueryable();

            // Filtrare după Nume
            if (!string.IsNullOrEmpty(searchName))
            {
                produse = produse.Where(p => p.Nume.Contains(searchName));
            }

            // Filtrare după Preț minim
            if (minPrice.HasValue)
            {
                produse = produse.Where(p => p.Pret >= minPrice.Value);
            }

            // Filtrare după Preț maxim
            if (maxPrice.HasValue)
            {
                produse = produse.Where(p => p.Pret <= maxPrice.Value);
            }

            // Filtrare după Cantitate minimă
            if (minQuantity.HasValue)
            {
                produse = produse.Where(p => p.Cantitate >= minQuantity.Value);
            }

            return View(await produse.ToListAsync());
        }

        // GET: Produs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produs
                .Include(p => p.Recenzii)
                .Include(p => p.Stoc)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // GET: Produs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Descriere,Pret,Cantitate")] Produs produs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produs);
        }

        // GET: Produs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produs.FindAsync(id);
            if (produs == null)
            {
                return NotFound();
            }
            return View(produs);
        }

        // POST: Produs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere,Pret,Cantitate")] Produs produs)
        {
            if (id != produs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdusExists(produs.Id))
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
            return View(produs);
        }

        // GET: Produs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // POST: Produs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produs = await _context.Produs.FindAsync(id);
            if (produs != null)
            {
                _context.Produs.Remove(produs);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProdusExists(int id)
        {
            return _context.Produs.Any(e => e.Id == id);
        }
    }
}
