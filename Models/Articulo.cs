using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pruebaTecnica.Models
{
    [Table("Articulos")]
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }

        [Required]
        [StringLength(15)]
        public string CodigoBarra { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Referencia { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        public string CodigoMarca { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(12)]
        public string? Talla { get; set; }

        [StringLength(6)]
        public string? CodigoColor { get; set; }

        [StringLength(12)]
        public string? Fabricante { get; set; }

        [StringLength(6)]
        public string? Categoria { get; set; }

        public byte? TipoImpuesto { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal PrecioDetal { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? PrecioMayor { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? PrecioAfiliado { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? PrecioPromocion { get; set; }

        public bool Promocion { get; set; }
    }
}
