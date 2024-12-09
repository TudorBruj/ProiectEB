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
    public class StocsController : Controller
    {
        private readonly ProiectEBContext _context;

        public StocsController(ProiectEBContext context)
        {
            _context = context;
        }

        // GET: Stocs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stoc.ToListAsync());
        }

        // GET: Stocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoc = await _context.Stoc
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
            return View();
        }

        // POST: Stocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(stoc);
        }

        // POST: Stocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StocExists(int id)
        {
            return _context.Stoc.Any(e => e.ID == id);
        }
    }
}
