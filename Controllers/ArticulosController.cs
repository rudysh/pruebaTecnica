using Microsoft.AspNetCore.Mvc;
using pruebaTecnica.Repositories;
using pruebaTecnica.ViewModels;

namespace pruebaTecnica.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly ILogger<ArticulosController> _logger;

        public ArticulosController(
            IArticuloRepository articuloRepository,
            ILogger<ArticulosController> logger
        )
        {
            _articuloRepository = articuloRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articulos = new ConsultaArticuloViewModel
            {
                Resultados = await _articuloRepository.ObtenerTodosAsync(),
                ConsultaRealizada = false
            };

            return View(articulos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Consultar(
            ConsultaArticuloViewModel consultaArticulo
        )
        {
            if (!ModelState.IsValid)
            {
                consultaArticulo.Resultados =
                    await _articuloRepository.ObtenerTodosAsync();

                return View("Index", consultaArticulo);
            }

            try
            {
                consultaArticulo.Referencia = consultaArticulo.Referencia.Trim();
                consultaArticulo.CodigoMarca = consultaArticulo.CodigoMarca.Trim();
                consultaArticulo.ConsultaRealizada = true;

                consultaArticulo.Resultados =
                    await _articuloRepository.ConsultarAsync(
                        consultaArticulo.Referencia,
                        consultaArticulo.CodigoMarca
                    );

                if (consultaArticulo.Resultados.Count == 0)
                {
                    ViewBag.Mensaje =
                        "No se encontraron artículos con los datos indicados.";
                }
                else
                {
                    ViewBag.Mensaje =
                        $"Se encontraron {consultaArticulo.Resultados.Count} artículos.";
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Error consultando artículos."
                );

                ViewBag.Error =
                    "Ocurrió un error al consultar los artículos.";

                consultaArticulo.Resultados =
                    await _articuloRepository.ObtenerTodosAsync();
            }

            return View("Index", consultaArticulo);
        }
    }
}
