using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using BeFit.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BeFit.Controllers
{
    public class CwiczeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        public CwiczeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cwiczenia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cwiczenia.Include(c => c.SesjaCwiczenia);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Cwiczenia/Create
        public IActionResult Create()
        {
            ViewData["TypCwiczeniaId"] = new SelectList(_context.TypCwiczenia, "Id", "Name");
            ViewData["SesjaCwiczeniaId"] = new SelectList(_context.SesjeCwiczenia, "Id", "Start");
            return View();
        }

        // POST: Cwiczenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CwiczeniaDTO model)
        {
            var userId = GetUserId();

            bool sesjaOk = await _context.Cwiczenia
                .AnyAsync(s => s.Id == model.SesjaCwiczeniaId && s.CreatedById == userId);

            Cwiczenia cwiczenia = new Cwiczenia()
            {
                SesjaCwiczeniaId = model.SesjaCwiczeniaId,
                TypCwiczeniaId = model.TypCwiczeniaId,
                Ciezar = model.Ciezar,
                Seria = model.Seria,
                Powtorzenia = model.Powtorzenia,
                CreatedById = userId
            };
            if (ModelState.IsValid)
            {
                _context.Add(cwiczenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypCwiczeniaId"] = new SelectList(_context.TypCwiczenia, "Id", "Name", model.TypCwiczeniaId);
            ViewData["SesjaCwiczeniaId"] = new SelectList(_context.SesjeCwiczenia.Where(s => s.CreatedById == userId), "Id", "Start", model.SesjaCwiczeniaId);
            
            var viewModel = new Cwiczenia
            {
                SesjaCwiczeniaId = model.SesjaCwiczeniaId,
                TypCwiczeniaId = model.TypCwiczeniaId,
                Ciezar = model.Ciezar,
                Seria = model.Seria,
                Powtorzenia = model.Powtorzenia
            };

            return View(viewModel);
        }

        // GET: Cwiczenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();

            var cwiczenia = await _context.Cwiczenia
                .FirstOrDefaultAsync(m => m.Id==id);

            if (cwiczenia == null)
            {
                return NotFound();
            }
            ViewData["TypCwiczeniaId"] = new SelectList(_context.TypCwiczenia, "Id", "Name", cwiczenia.TypCwiczeniaId);
            ViewData["SesjaCwiczeniaId"] = new SelectList(_context.SesjeCwiczenia.Where(s => s.CreatedById == userId), "Id", "Start", cwiczenia.SesjaCwiczeniaId);
            return View(cwiczenia);
        }

        // POST: Cwiczenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SesjaCwiczeniaId,TypCwiczeniaId,Ciezar,Seria,Powtorzenia,CreatedById")] CwiczeniaDTO cwiczeniaDTO)
        {
            if (id != cwiczeniaDTO.Id)
            {
                return NotFound();
            }

            Cwiczenia cwiczenia = new Cwiczenia()
            {
                Id = cwiczeniaDTO.Id,
                SesjaCwiczeniaId = cwiczeniaDTO.SesjaCwiczeniaId,
                TypCwiczeniaId = cwiczeniaDTO.TypCwiczeniaId,
                Ciezar = cwiczeniaDTO.Ciezar,
                Seria = cwiczeniaDTO.Seria,
                Powtorzenia = cwiczeniaDTO.Powtorzenia,
                CreatedById = GetUserId()
            };

            if (!CwiczeniaExists(cwiczenia.Id, GetUserId()))
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
                    if (!CwiczeniaExists(cwiczenia.Id,GetUserId()))
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
            ViewData["TypCwiczeniaId"] = new SelectList(_context.TypCwiczenia, "Id", "Name", cwiczenia.TypCwiczeniaId);
            ViewData["SesjaCwiczeniaId"] = new SelectList(_context.SesjeCwiczenia, "Id", "Start", cwiczenia.SesjaCwiczeniaId);
            return View(cwiczenia);
        }

        // GET: Cwiczenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenia = await _context.Cwiczenia
                .Where(e => e.CreatedById == GetUserId())
                .Include(e => e.TypCwiczenia)
                .Include(e=> e.SesjaCwiczenia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cwiczenia == null)
            {
                return NotFound();
            }

            return View(cwiczenia);
        }

        // POST: Cwiczenia/Delete/5
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

        private bool CwiczeniaExists(int id, string userId)
        {
            return _context.Cwiczenia.Any(e => e.Id == id && e.CreatedById == userId);
        }
    }
}
