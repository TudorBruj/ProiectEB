using System;
using System.Collections.Generic;
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
    public class ComandasController : Controller
    {
        private readonly ProiectEBContext _context;

        public ComandasController(ProiectEBContext context)
        {
            _context = context;
        }

        // GET: Comandas
        public async Task<IActionResult> Index(string searchClient, string searchDate)
        {
            // Obținem comenzile incluzând datele clientului
            var comenzi = _context.Comanda.Include(c => c.Client).AsQueryable();

            // Filtrare după Numele Clientului
            if (!string.IsNullOrEmpty(searchClient))
            {
                comenzi = comenzi.Where(c => c.Client.Nume.Contains(searchClient));
            }

            // Filtrare după Data Comenzii
            if (!string.IsNullOrEmpty(searchDate))
            {
                if (DateTime.TryParse(searchDate, out DateTime parsedDate))
                {
                    comenzi = comenzi.Where(c => c.DataComenzii.Date == parsedDate.Date);
                }
            }

            // Returnăm rezultatele către View
            return View(await comenzi.ToListAsync());
        }

        // GET: Comandas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comanda
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // GET: Comandas/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Nume");
            return View();
        }

        // POST: Comandas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,DataComenzii,TotalPlatit")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Nume", comanda.ClientId);
            return View(comanda);
        }

        // GET: Comandas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comanda.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Nume", comanda.ClientId);
            return View(comanda);
        }

        // POST: Comandas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,DataComenzii,TotalPlatit")] Comanda comanda)
        {
            if (id != comanda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComandaExists(comanda.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Nume", comanda.ClientId);
            return View(comanda);
        }

        // GET: Comandas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comanda
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // POST: Comandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comanda = await _context.Comanda.FindAsync(id);
            if (comanda != null)
            {
                _context.Comanda.Remove(comanda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComandaExists(int id)
        {
            return _context.Comanda.Any(e => e.Id == id);
        }
    }
}

