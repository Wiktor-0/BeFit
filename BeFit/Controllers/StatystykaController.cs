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
using System.Data;
using System.Security.Claims;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatystykaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatystykaController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        // GET: Statystyka
        public async Task<IActionResult> Index()
        {
            DateTime fromDate = DateTime.Now.AddDays(-28);

            var statystyka = await _context.Cwiczenia
                .Where(s => s.SesjaCwiczenia.Start >= fromDate)
                .Where(s => s.CreatedById == GetUserId())
                .GroupBy(s => s.TypCwiczenia.Name)
                .Select(g => new Statystyka
                {
                    Nazwa = g.Key,
                    Liczba = g.Count(),
                    AllPowtorzenia = g.Sum(x => x.Seria * x.Powtorzenia),
                    SredniCieraz = g.Average(x => x.Ciezar),
                    MaxCiezar = g.Max(x => x.Ciezar),
                })
                .ToListAsync();


            return View(statystyka);
        }
    }
}