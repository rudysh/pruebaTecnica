namespace pruebaTecnica.Models
{
    public class ArticuloResultado
    {
        public int IdArticulo { get; set; }

        public string CodigoBarra { get; set; } = string.Empty;

        public string Referencia { get; set; } = string.Empty;

        public string CodigoMarca { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public decimal PrecioDetal { get; set; }
    }
}
