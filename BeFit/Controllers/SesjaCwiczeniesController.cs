using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    public class SesjaCwiczeniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SesjaCwiczeniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SesjaCwiczenies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SesjeCwiczenia.Include(s => s.Cwiczenia).Include(s => s.Sesja);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SesjaCwiczenies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaCwiczenie = await _context.SesjeCwiczenia
                .Include(s => s.Cwiczenia)
                .Include(s => s.Sesja)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesjaCwiczenie == null)
            {
                return NotFound();
            }

            return View(sesjaCwiczenie);
        }

        // GET: SesjaCwiczenies/Create
        public IActionResult Create()
        {
            ViewData["CwiczeniaId"] = new SelectList(_context.Cwiczenia, "Id", "Name");
            ViewData["SesjaId"] = new SelectList(_context.Sesje, "Id", "Name");
            return View();
        }

        // POST: SesjaCwiczenies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SesjaId,CwiczeniaId,CiezarKg,Serie,Powtorzenia")] SesjaCwiczenie sesjaCwiczenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesjaCwiczenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CwiczeniaId"] = new SelectList(_context.Cwiczenia, "Id", "Name", sesjaCwiczenie.CwiczeniaId);
            ViewData["SesjaId"] = new SelectList(_context.Sesje, "Id", "Name", sesjaCwiczenie.SesjaId);
            return View(sesjaCwiczenie);
        }

        // GET: SesjaCwiczenies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaCwiczenie = await _context.SesjeCwiczenia.FindAsync(id);
            if (sesjaCwiczenie == null)
            {
                return NotFound();
            }
            ViewData["CwiczeniaId"] = new SelectList(_context.Cwiczenia, "Id", "Name", sesjaCwiczenie.CwiczeniaId);
            ViewData["SesjaId"] = new SelectList(_context.Sesje, "Id", "Name", sesjaCwiczenie.SesjaId);
            return View(sesjaCwiczenie);
        }

        // POST: SesjaCwiczenies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SesjaId,CwiczeniaId,CiezarKg,Serie,Powtorzenia")] SesjaCwiczenie sesjaCwiczenie)
        {
            if (id != sesjaCwiczenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesjaCwiczenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesjaCwiczenieExists(sesjaCwiczenie.Id))
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
            ViewData["CwiczeniaId"] = new SelectList(_context.Cwiczenia, "Id", "Name", sesjaCwiczenie.CwiczeniaId);
            ViewData["SesjaId"] = new SelectList(_context.Sesje, "Id", "Naem", sesjaCwiczenie.SesjaId);
            return View(sesjaCwiczenie);
        }

        // GET: SesjaCwiczenies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaCwiczenie = await _context.SesjeCwiczenia
                .Include(s => s.Cwiczenia)
                .Include(s => s.Sesja)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesjaCwiczenie == null)
            {
                return NotFound();
            }

            return View(sesjaCwiczenie);
        }

        // POST: SesjaCwiczenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesjaCwiczenie = await _context.SesjeCwiczenia.FindAsync(id);
            if (sesjaCwiczenie != null)
            {
                _context.SesjeCwiczenia.Remove(sesjaCwiczenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesjaCwiczenieExists(int id)
        {
            return _context.SesjeCwiczenia.Any(e => e.Id == id);
        }
    }
}
