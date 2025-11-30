using BeFit.Models;

namespace BeFit.DTO
{
    public class SesjaCwiczeniaDTO
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Koniec { get; set; }

        public SesjaCwiczeniaDTO() { }
        public SesjaCwiczeniaDTO(SesjaCwiczenia sesjaCwiczenia)
        {
            Id = sesjaCwiczenia.Id;
            Start = sesjaCwiczenia.Start;
            Koniec = sesjaCwiczenia.Koniec;
        }
    }
}
