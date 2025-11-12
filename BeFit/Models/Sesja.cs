using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Sesja
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Data i godzina rozpoczęcia")]
        public DateTime Start { get; set; }
        [Required]
        [Display(Name = "Data i godzina zakończenia")]
        public DateTime End { get; set; }
        public List<SesjaCwiczenie> SesjaCwiczenies { get; set; } = new();
    }
}
