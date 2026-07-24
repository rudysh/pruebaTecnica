using pruebaTecnica.Models;

namespace pruebaTecnica.Repositories
{
    public interface IArticuloRepository
    {
        Task<List<ArticuloResultado>> ObtenerTodosAsync();

        Task<List<ArticuloResultado>> ConsultarAsync(
            string referencia,
            string codigoMarca
        );
    }
}
