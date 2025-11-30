using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TypCwiczenia
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa ćwiczenia")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
