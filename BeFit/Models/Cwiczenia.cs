using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Cwiczenia
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Opis ćwiczenia")]
        public string Name { get; set; }
    }
}
