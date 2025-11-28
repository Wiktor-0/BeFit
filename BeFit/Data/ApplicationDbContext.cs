using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cwiczenia> Cwiczenia { get; set; } = default!;
        public DbSet<TypCwiczenia> TypCwiczenia { get; set; } = default!;
        public DbSet<SesjaCwiczenia> SesjeCwiczenia { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "500", Name = "Admin", NormalizedName = "ADMIN" });
        }
    }
}
