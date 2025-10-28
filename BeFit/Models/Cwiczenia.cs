using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Cwiczenia
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
