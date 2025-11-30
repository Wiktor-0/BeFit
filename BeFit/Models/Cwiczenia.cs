using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Cwiczenia
    {
        public int Id { get; set; }

        // Relacja do SesjaCwiczenia
        [Display(Name = "Sesja treningowa")]
        public int SesjaCwiczeniaId { get; set; }
        [Display(Name = "Sesja treningowa")]
        public virtual SesjaCwiczenia? SesjaCwiczenia { get; set; }

        // Relacja do TypCwiczenia
        [Display(Name = "Typ ćwiczenia")]
        public int TypCwiczeniaId { get; set; }
        [Display(Name = "Typ ćwiczenia")]
        public virtual TypCwiczenia? TypCwiczenia { get; set; }

        // Obciążenie w kg (może być 0 jeśli ćwiczenie bez obciążenia)
        [Range(0, 1000, ErrorMessage = "Zakres obciążenia 0 - 1000 kg")]
        [Display(Name = "Obciążenie (kg)")]
        public int Ciezar { get; set; }

        [Range(1, 100, ErrorMessage = "Zakres serii 1 - 100")]
        [Display(Name = "Serie")]
        public int Seria { get; set; }

        [Range(1, 10000, ErrorMessage = "Zakres powtórzeń 1 - 10000")]
        [Display(Name = "Powtórzenia")]
        public int Powtorzenia { get; set; }

        [Display(Name = "Stworzone przez")]
        public string CreatedById { get; set; } = string.Empty;
        [Display(Name = "Stworzone przez")]
        public virtual AppUser? CreatedBy { get; set; }
    }
}
