using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeFit.Models;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cwiczenia> Cwiczenia { get; set; } = default!;
        public DbSet<Sesja> Sesje { get; set; } = default!;
        public DbSet<SesjaCwiczenie> SesjeCwiczenia { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // (opcjonalnie) indeks/unikalność nazwy ćwiczenia
            builder.Entity<Cwiczenia>()
                   .HasIndex(e => e.Name);

            // relacje SesjaCwiczenie -> Sesja (wiele zapisów w jednej sesji)
            builder.Entity<SesjaCwiczenie>()
                   .HasOne(sc => sc.Sesja)
                   .WithMany()                 // możesz dodać w Sesja: public List<SesjaCwiczenie> Pozycje { get; set; } = new();
                   .HasForeignKey(sc => sc.SesjaId)
                   .OnDelete(DeleteBehavior.Cascade);

            // relacje SesjaCwiczenie -> Cwiczenia (konkretny typ ćwiczenia)
            builder.Entity<SesjaCwiczenie>()
                   .HasOne(sc => sc.Cwiczenia)
                   .WithMany()
                   .HasForeignKey(sc => sc.CwiczeniaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
