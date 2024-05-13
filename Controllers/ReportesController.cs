using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashBoard_API_BiblioPro.Context;
using DashBoard_API_BiblioPro.Models;

namespace DashBoard_API_BiblioPro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ContextoBiblioPro _contexto;

        public ReportesController(ContextoBiblioPro contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        [Route("GetAllLibros")]
        public async Task<IActionResult> GetAllLibros()
        {
          var libros = await _contexto.Libros.
                Include(libro => libro.Categoria).
                Include(libro => libro.Editorial).
                ToListAsync();

            if (libros == null)
            {
                return NotFound("No se encontraron libros");
            }
            return Ok(libros);
        }



        //Trae los 5 libros mas prestados en toda la historia junto a su ultima fecha de prestamo
        [HttpGet]
        [Route("GetTop5LibrosMasPrestados")]
        public async Task<IActionResult> GetTop5LibrosMasPrestados()
        {
            var libros = await _contexto.DetallesPrestamos.
                Include(detalle => detalle.Libro).
                Include(detalle => detalle.Prestamo).
                GroupBy(detalle => detalle.Libro).
                Select(libro => new
                {
                    libro.Key.TituloLibro,
                    UltimaFechaPrestamo = libro.Max(detalle => detalle.Prestamo.FechaPrestamo),
                    Cantidad = libro.Count()
                }).
                OrderByDescending(libro => libro.Cantidad).
                Take(5).
                ToListAsync();

            if (libros == null)
            {
                return NotFound("No se encontraron libros");
            }
            return Ok(libros);
        }

        //Trae los 10 libros que mas se han prestado en un año especifico, recibe el año como parametro
        [HttpGet]
        [Route("GetTop10LibrosMasPrestadosAnio")]
        public async Task<IActionResult> GetTop10LibrosMasPrestadosAnio(int anio)
        {
            var libros = await _contexto.DetallesPrestamos.
                Include(detalle => detalle.Libro).
                Include(detalle => detalle.Prestamo).
                Where(detalle => detalle.Prestamo.FechaPrestamo.Year == anio).
                GroupBy(detalle => detalle.Libro).
                Select(libro => new
                {
                    libro.Key.TituloLibro,
                    Cantidad = libro.Count()
                }).
                OrderByDescending(libro => libro.Cantidad).
                Take(10).
                ToListAsync();

            if (libros == null)
            {
                return NotFound("No se encontraron libros");
            }
            return Ok(libros);
        }

        //Trae los años en los que se han realizado prestamos
        [HttpGet]
        [Route("GetAniosPrestamos")]
        public async Task<IActionResult> GetAniosPrestamos()
        {
            var anios = await _contexto.DetallesPrestamos.
                Select(detalle => detalle.Prestamo.FechaPrestamo.Year).
                Distinct().
                ToListAsync();

            if (anios == null)
            {
                return NotFound("No se encontraron años");
            }
            return Ok(anios);
        }


    }
}
