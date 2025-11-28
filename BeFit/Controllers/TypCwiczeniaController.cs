using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeFit.Controllers
{
    public class TypCwiczeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypCwiczeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypCwiczenia
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypCwiczenia.ToListAsync());
        }

        // GET: TypCwiczenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typCwiczenia = await _context.TypCwiczenia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typCwiczenia == null)
            {
                return NotFound();
            }

            return View(typCwiczenia);
        }

        // GET: TypCwiczenia/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypCwiczenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypCwiczenia typCwiczenia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typCwiczenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typCwiczenia);
        }

        // GET: TypCwiczenia/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typCwiczenia = await _context.TypCwiczenia.FindAsync(id);
            if (typCwiczenia == null)
            {
                return NotFound();
            }
            return View(typCwiczenia);
        }

        // POST: TypCwiczenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypCwiczenia typCwiczenia)
        {
            if (id != typCwiczenia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typCwiczenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypCwiczeniaExists(typCwiczenia.Id))
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
            return View(typCwiczenia);
        }

        // GET: TypCwiczenia/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typCwiczenia = await _context.TypCwiczenia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typCwiczenia == null)
            {
                return NotFound();
            }

            return View(typCwiczenia);
        }

        // POST: TypCwiczenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typCwiczenia = await _context.TypCwiczenia.FindAsync(id);
            if (typCwiczenia != null)
            {
                _context.TypCwiczenia.Remove(typCwiczenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypCwiczeniaExists(int id)
        {
            return _context.TypCwiczenia.Any(e => e.Id == id);
        }
    }
}
