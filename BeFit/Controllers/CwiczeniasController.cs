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
    public class CwiczeniasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CwiczeniasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cwiczenias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cwiczenia.ToListAsync());
        }

        // GET: Cwiczenias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenia = await _context.Cwiczenia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cwiczenia == null)
            {
                return NotFound();
            }

            return View(cwiczenia);
        }

        // GET: Cwiczenias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cwiczenias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Cwiczenia cwiczenia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cwiczenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cwiczenia);
        }

        // GET: Cwiczenias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenia = await _context.Cwiczenia.FindAsync(id);
            if (cwiczenia == null)
            {
                return NotFound();
            }
            return View(cwiczenia);
        }

        // POST: Cwiczenias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Cwiczenia cwiczenia)
        {
            if (id != cwiczenia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cwiczenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CwiczeniaExists(cwiczenia.Id))
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
            return View(cwiczenia);
        }

        // GET: Cwiczenias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenia = await _context.Cwiczenia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cwiczenia == null)
            {
                return NotFound();
            }

            return View(cwiczenia);
        }

        // POST: Cwiczenias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cwiczenia = await _context.Cwiczenia.FindAsync(id);
            if (cwiczenia != null)
            {
                _context.Cwiczenia.Remove(cwiczenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CwiczeniaExists(int id)
        {
            return _context.Cwiczenia.Any(e => e.Id == id);
        }
    }
}
