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
    public class RecenziesController : Controller
    {
        private readonly ProiectEBContext _context;

        public RecenziesController(ProiectEBContext context)
        {
            _context = context;
        }

        // GET: Recenzies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recenzie.ToListAsync());
        }

        // GET: Recenzies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzie = await _context.Recenzie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recenzie == null)
            {
                return NotFound();
            }

            return View(recenzie);
        }

        // GET: Recenzies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recenzies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdClient,IdProdus,Evaluare,Comentariu")] Recenzie recenzie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recenzie);
        }

        // GET: Recenzies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzie = await _context.Recenzie.FindAsync(id);
            if (recenzie == null)
            {
                return NotFound();
            }
            return View(recenzie);
        }

        // POST: Recenzies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdClient,IdProdus,Evaluare,Comentariu")] Recenzie recenzie)
        {
            if (id != recenzie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzieExists(recenzie.ID))
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
            return View(recenzie);
        }

        // GET: Recenzies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzie = await _context.Recenzie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recenzie == null)
            {
                return NotFound();
            }

            return View(recenzie);
        }

        // POST: Recenzies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzie = await _context.Recenzie.FindAsync(id);
            if (recenzie != null)
            {
                _context.Recenzie.Remove(recenzie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzieExists(int id)
        {
            return _context.Recenzie.Any(e => e.ID == id);
        }
    }
}
