using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using System.Security.Claims;
using BeFit.DTO;

namespace BeFit.Controllers
{
    public class SesjaCwiczeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        public SesjaCwiczeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SesjaCwiczenia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SesjeCwiczenia
                .Where(s => s.CreatedById == GetUserId())
                .Include(s => s.Cwiczenia)
                .Select(s => new SesjaCwiczeniaDTO(s)).ToListAsync();

            return View(await applicationDbContext);
        }

        // GET: SesjaCwiczenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaCwiczenia = await _context.SesjeCwiczenia
                .Where(s => s.CreatedById == GetUserId())
                .Include(s => s.Cwiczenia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesjaCwiczenia == null)
            {
                return NotFound();
            }

            return View(sesjaCwiczenia);
        }

        // GET: SesjaCwiczenia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SesjaCwiczenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SesjaCwiczeniaDTO sesjaCwiczeniaDTO)
        {
            SesjaCwiczenia sesjaCwiczenia = new SesjaCwiczenia
            {
                Start = sesjaCwiczeniaDTO.Start,
                End = sesjaCwiczeniaDTO.Koniec,
                CreatedById = GetUserId()
            };
            if (ModelState.IsValid)
            {
                _context.Add(sesjaCwiczenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sesjaCwiczenia);
        }

        // GET: SesjaCwiczenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaCwiczenia = await _context.SesjeCwiczenia
                .Where(e => e.CreatedById == GetUserId())
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesjaCwiczenia == null)
            {
                return NotFound();
            }
            return View(sesjaCwiczenia);
        }

        // POST: SesjaCwiczenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start,Koniec")] SesjaCwiczeniaDTO sesjaCwiczeniaDTO)
        {
            if (id != sesjaCwiczeniaDTO.Id)
            {
                return NotFound();
            }

            SesjaCwiczenia sesjaCwiczenia = new SesjaCwiczenia
            {
                Start = sesjaCwiczeniaDTO.Start,
                End = sesjaCwiczeniaDTO.Koniec,
                Cwiczenia = new List<Cwiczenia>(),
                CreatedById = GetUserId()
            };

            if (!SesjaCwiczeniaExists(sesjaCwiczenia.Id, GetUserId()))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesjaCwiczenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesjaCwiczeniaExists(sesjaCwiczenia.Id,GetUserId()))
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
            return View(sesjaCwiczenia);
        }

        //// GET: SesjaCwiczenia/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sesjaCwiczenia = await _context.SesjeCwiczenia
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (sesjaCwiczenia == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sesjaCwiczenia);
        //}

        // POST: SesjaCwiczenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesjaCwiczenia = await _context.SesjeCwiczenia.FindAsync(id);
            if (sesjaCwiczenia != null)
            {
                _context.SesjeCwiczenia.Remove(sesjaCwiczenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesjaCwiczeniaExists(int id,string userId)
        {
            return _context.SesjeCwiczenia.Any(e => e.Id == id && e.CreatedById == userId);
        }
    }
}
