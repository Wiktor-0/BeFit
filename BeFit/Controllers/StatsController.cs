using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;

namespace BeFit.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public StatsController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // GET: /Stats
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Zakres: ostatnie 4 tygodnie (28 dni)
            var od = DateTime.Now.Date.AddDays(-28);

            // Pobieramy dane z powiązanych tabel
            var model = await _ctx.SesjeCwiczenia
                .AsNoTracking()
                .Include(x => x.Sesja)
                .Include(x => x.Cwiczenia)
                .Where(x => x.Sesja != null && x.Sesja.Start >= od)
                .GroupBy(x => new { x.CwiczeniaId, Nazwa = x.Cwiczenia!.Name })
                .Select(g => new
                {
                    g.Key.CwiczeniaId,
                    g.Key.Nazwa,

                    // Ile razy w ciągu ostatnich 4 tygodni wykonywano dane ćwiczenie
                    IleRazyWykonywane = g.Select(s => s.SesjaId).Distinct().Count(),

                    // Łączna liczba powtórzeń (serie * powtórzenia)
                    LaczniePowtorzen = g.Sum(s => s.Serie * s.Powtorzenia),

                    // Średnie i maksymalne obciążenie
                    SrednieObciazenieKg = g.Average(s => s.CiezarKg),
                    MaksymalneObciazenieKg = g.Max(s => s.CiezarKg)
                })
                .OrderBy(r => r.Nazwa)
                .ToListAsync();

            return View(model);
        }
    }
}
