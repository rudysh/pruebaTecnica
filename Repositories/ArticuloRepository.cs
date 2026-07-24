using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Data;
using pruebaTecnica.Models;

namespace pruebaTecnica.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticuloRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArticuloResultado>> ObtenerTodosAsync()
        {
            return await _context.Articulos
                .AsNoTracking()
                .OrderBy(articulo => articulo.Referencia)
                .ThenBy(articulo => articulo.CodigoMarca)
                .ThenBy(articulo => articulo.IdArticulo)
                .Select(articulo => new ArticuloResultado
                {
                    IdArticulo = articulo.IdArticulo,
                    CodigoBarra = articulo.CodigoBarra,
                    Referencia = articulo.Referencia,
                    CodigoMarca = articulo.CodigoMarca,
                    Nombre = articulo.Nombre,
                    PrecioDetal = articulo.PrecioDetal
                })
                .ToListAsync();
        }

        public async Task<List<ArticuloResultado>> ConsultarAsync(
            string referencia,
            string codigoMarca
        )
        {
            return await _context.ArticulosResultado
                .FromSqlInterpolated($"""
                EXEC dbo.SP_ConsultarArticulo
                    @Referencia = {referencia},
                    @CodigoMarca = {codigoMarca}
                """)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
