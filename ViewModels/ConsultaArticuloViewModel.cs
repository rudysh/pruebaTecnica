using pruebaTecnica.Models;
using System.ComponentModel.DataAnnotations;

namespace pruebaTecnica.ViewModels
{
    public class ConsultaArticuloViewModel
    {
        [Required(ErrorMessage = "Debe ingresar una referencia.")]
        [StringLength(
            20,
            ErrorMessage = "La referencia no puede superar los 20 caracteres."
        )]
        [Display(Name = "Referencia")]
        public string Referencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el código de marca.")]
        [StringLength(
            6,
            ErrorMessage = "El código de marca no puede superar los 6 caracteres."
        )]
        [Display(Name = "Código de marca")]
        public string CodigoMarca { get; set; } = string.Empty;

        public List<ArticuloResultado> Resultados { get; set; } = [];

        public bool ConsultaRealizada { get; set; }
    }
}
