using System.ComponentModel.DataAnnotations;

namespace OneMatter.Models.ViewModels
{
    public class CreateJobViewModel
    {
        [Required(ErrorMessage = "O título da vaga é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        [Display(Name = "Título da Vaga")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [Display(Name = "Descrição da Vaga")]
        [DataType(DataType.MultilineText)] // Isso ajudará o MVC a renderizar um <textarea>
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "A localização é obrigatória.")]
        [StringLength(50, ErrorMessage = "A localização deve ter no máximo 50 caracteres.")]
        [Display(Name = "Localização (ex: Remoto, Híbrido, São Paulo)")]
        public string Location { get; set; } = string.Empty;
    }
}