using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashBoard_API_BiblioPro.Context;

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

        //Trae los 5 libros que mas se ha prestado
        [HttpGet]
        [Route("GetLibrosMasPrestados")]
        public async Task<IActionResult> GetLibroMasPrestado()
        {         
            var libros = await _contexto.DetallesPrestamos.
                Include(detalle => detalle.IdLibro).
                ThenInclude(libro => libro.Categoria).
                Include(detalle => detalle.IdLibro).
                ThenInclude(libro => libro.Editorial).
                GroupBy(detalle => detalle.IdLibro).
                Select(group => new
                {
                    Libro = group.Key,
                    Cantidad = group.Count()
                }).
                OrderByDescending(libro => libro.Cantidad).
                Take(5).
                ToListAsync();
            foreach (var libro in libros)
            {
                libro.Libro.Categoria = await _contexto.Categorias.FindAsync(libro.Libro.IdCategoria);
                libro.Libro.Editorial = await _contexto.Editoriales.FindAsync(libro.Libro.IdEditorial);
            }

            if (libros == null)
            {
                return NotFound("No se encontraron libros");
            }
            return Ok(libros);
        }

    }
}
