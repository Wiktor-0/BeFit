using BeFit.Models;

namespace BeFit.DTO
{
    public class CwiczeniaDTO
    {
        public int Id { get; set; }
        public int SesjaCwiczeniaId { get; set; }
        public int TypCwiczeniaId { get; set; }
        public int Ciezar { get; set; }
        public int Seria { get; set; }
        public int Powtorzenia { get; set; }
        public CwiczeniaDTO() { }
        public CwiczeniaDTO(Cwiczenia cwiczenia)
        {
            Id = cwiczenia.Id;
            SesjaCwiczeniaId = cwiczenia.SesjaCwiczeniaId;
            TypCwiczeniaId = cwiczenia.TypCwiczeniaId;
            Ciezar = cwiczenia.Ciezar;
            Seria = cwiczenia.Seria;
            Powtorzenia = cwiczenia.Powtorzenia;
        }
    }
}
