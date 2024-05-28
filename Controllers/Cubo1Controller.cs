using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashBoard_API_BiblioPro.Context;
using DashBoard_API_BiblioPro.Models;

namespace DashBoard_API_BiblioPro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cubo1Controller : ControllerBase
    {
        private readonly ContextoBiblioPro _contexto;

        public Cubo1Controller(ContextoBiblioPro contexto)
        {
            _contexto = contexto;
        }


        //Trae todos los libros que tengan prestamos
        //Retorna el nombre del libro y su id
        [HttpGet]
        [Route("GetLibrosConPrestamos")]
        public async Task<IActionResult> GetLibrosConPrestamos()
        {
            var libros = await _contexto.DetallesPrestamos.
                Include(detalle => detalle.Libro).
                GroupBy(detalle => detalle.Libro).
                Select(libro => new
                {
                    libro.Key.IdLibro,
                    libro.Key.TituloLibro
                }).
                ToListAsync();

            if (libros == null)
            {
                return NotFound("No se encontraron libros");
            }
            return Ok(libros);
        }

        //Trae las categorias de los libros que tengan prestamos
        //Retorna el nombre de la categoria y su id
        [HttpGet]
        [Route("GetCategoriasConPrestamos")]
        public async Task<IActionResult> GetCategoriasConPrestamos()
        {
            var categorias = await _contexto.DetallesPrestamos.
                Include(detalle => detalle.Libro.Categoria).
                GroupBy(detalle => detalle.Libro.Categoria).
                Select(categoria => new
                {
                    categoria.Key.IdCategoria,
                    categoria.Key.NombreCategoria
                }).
                ToListAsync();

            if (categorias == null)
            {
                return NotFound("No se encontraron categorias");
            }
            return Ok(categorias);
        }

        //Trae los libros mas prestados de cada año
        //De esos libros, se traera el nombre del libro y la cantidad de veces que fue prestado por cada mes
        //Recibe un año como parametro y una categoria, si no recibe categoria, traera los libros de todas las categorias
        [HttpGet]
        [Route("GetTopLibrosMasPrestadosPorAnio")]
        public async Task<IActionResult> GetTopLibrosMasPrestadosPorAnioYCategoria(int anio, int categoria)
        {
            if (anio == 0 && categoria == 0)
            {
                var libros = await _contexto.DetallesPrestamos.
                 Include(detalle => detalle.Libro).
                 Include(detalle => detalle.Prestamo).
                 GroupBy(detalle => detalle.Libro).
                 Select(libro => new
                 {
                     libro.Key.TituloLibro,
                     Enero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 1),
                     Febrero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 2),
                     Marzo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 3),
                     Abril = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 4),
                     Mayo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 5),
                     Junio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 6),
                     Julio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 7),
                     Agosto = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 8),
                     Septiembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 9),
                     Octubre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 10),
                     Noviembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 11),
                     Diciembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 12),
                     Total = libro.Count()
                 }).
                 OrderByDescending(libro => libro.Total).
                 Take(25).
                 ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);

            }
            else if (anio == 0 && categoria != 0)
            {
                var libros = await _contexto.DetallesPrestamos.
                   Include(detalle => detalle.Libro).
                   Include(detalle => detalle.Prestamo).
                   Where(detalle => detalle.Libro.Categoria.IdCategoria == categoria).
                   GroupBy(detalle => detalle.Libro).
                   Select(libro => new
                   {
                       libro.Key.TituloLibro,
                       Enero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 1),
                       Febrero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 2),
                       Marzo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 3),
                       Abril = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 4),
                       Mayo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 5),
                       Junio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 6),
                       Julio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 7),
                       Agosto = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 8),
                       Septiembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 9),
                       Octubre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 10),
                       Noviembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 11),
                       Diciembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 12),
                       Total = libro.Count()
                   }).
                   OrderByDescending(libro => libro.Total).
                   Take(25).
                   ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
            else if (anio != 0 && categoria == 0)
            {
                var libros = await _contexto.DetallesPrestamos.
                  Include(detalle => detalle.Libro).
                  Include(detalle => detalle.Prestamo).
                  Where(detalle => detalle.Prestamo.FechaPrestamo.Year == anio).
                  GroupBy(detalle => detalle.Libro).
                  Select(libro => new
                  {
                      libro.Key.TituloLibro,
                      Enero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 1),
                      Febrero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 2),
                      Marzo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 3),
                      Abril = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 4),
                      Mayo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 5),
                      Junio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 6),
                      Julio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 7),
                      Agosto = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 8),
                      Septiembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 9),
                      Octubre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 10),
                      Noviembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 11),
                      Diciembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 12),
                      Total = libro.Count()
                  }).
                  OrderByDescending(libro => libro.Total).
                  Take(25).
                  ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
            else
            {
                var libros = await _contexto.DetallesPrestamos.
                    Include(detalle => detalle.Libro).
                    Include(detalle => detalle.Prestamo).
                    Where(detalle => detalle.Prestamo.FechaPrestamo.Year == anio && detalle.Libro.Categoria.IdCategoria == categoria).
                    GroupBy(detalle => detalle.Libro).
                    Select(libro => new
                    {
                        libro.Key.TituloLibro,
                        Enero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 1),
                        Febrero = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 2),
                        Marzo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 3),
                        Abril = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 4),
                        Mayo = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 5),
                        Junio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 6),
                        Julio = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 7),
                        Agosto = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 8),
                        Septiembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 9),
                        Octubre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 10),
                        Noviembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 11),
                        Diciembre = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Month == 12),
                        Total = libro.Count()
                    }).
                    OrderByDescending(libro => libro.Total).
                    Take(25).
                    ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
        }

        //Trae los libros que mas se han prestado en la historia
        //Recibe un mes como parametro y una categoria, si no recibe categoria, traera los libros de todas las categorias
        //Consultara desde el año 2020 hasta el año actual(2024)
        //Retornara el nombre del libro y la cantidad de veces que fue prestado en el mes especificado
        //A lo largo de los años
        [HttpGet]
        [Route("GetTopLibrosMasPrestadosPorMes")]
        public async Task<IActionResult> GetTopLibrosMasPrestadosPorMes(int mes, int categoria)
        {
            if (mes == 0 && categoria == 0)
            {
                var libros = await _contexto.DetallesPrestamos.
                     Include(detalle => detalle.Libro).
                     Include(detalle => detalle.Prestamo).                   
                     GroupBy(detalle => detalle.Libro).
                     Select(libro => new
                     {
                         libro.Key.TituloLibro,
                         Anio2020 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2020),
                         Anio2021 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2021),
                         Anio2022 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2022),
                         Anio2023 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2023),
                         Anio2024 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2024),
                         Total = libro.Count()
                     }).
                     OrderByDescending(libro => libro.Total).
                     Take(25).
                     ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
            else if (mes == 0 && categoria != 0)
            {
                var libros = await _contexto.DetallesPrestamos.
                    Include(detalle => detalle.Libro).
                    Include(detalle => detalle.Prestamo).
                    Where(detalle => detalle.Libro.Categoria.IdCategoria == categoria).
                    GroupBy(detalle => detalle.Libro).
                    Select(libro => new
                    {
                        libro.Key.TituloLibro,
                        Anio2020 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2020),
                        Anio2021 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2021),
                        Anio2022 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2022),
                        Anio2023 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2023),
                        Anio2024 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2024),
                        Total = libro.Count()
                    }).
                    OrderByDescending(libro => libro.Total).
                    Take(25).
                    ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
            else if (mes != 0 && categoria == 0)
            {
                var libros = await _contexto.DetallesPrestamos.
                    Include(detalle => detalle.Libro).
                    Include(detalle => detalle.Prestamo).
                    Where(detalle => detalle.Prestamo.FechaPrestamo.Month == mes ).
                    GroupBy(detalle => detalle.Libro).
                    Select(libro => new
                    {
                        libro.Key.TituloLibro,
                        Anio2020 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2020),
                        Anio2021 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2021),
                        Anio2022 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2022),
                        Anio2023 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2023),
                        Anio2024 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2024),
                        Total = libro.Count()
                    }).
                    OrderByDescending(libro => libro.Total).
                    Take(25).
                    ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
            else {
                var libros = await _contexto.DetallesPrestamos.
                    Include(detalle => detalle.Libro).
                    Include(detalle => detalle.Prestamo).
                    Where(detalle => detalle.Prestamo.FechaPrestamo.Month == mes && detalle.Libro.Categoria.IdCategoria == categoria).
                    GroupBy(detalle => detalle.Libro).
                    Select(libro => new
                    {
                        libro.Key.TituloLibro,
                        Anio2020 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2020),
                        Anio2021 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2021),
                        Anio2022 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2022),
                        Anio2023 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2023),
                        Anio2024 = libro.Count(detalle => detalle.Prestamo.FechaPrestamo.Year == 2024),
                        Total = libro.Count()
                    }).
                    OrderByDescending(libro => libro.Total).
                    Take(25).
                    ToListAsync();

                if (libros == null)
                {
                    return NotFound("No se encontraron libros");
                }
                return Ok(libros);
            }
        }




        //Trae, de un libro en especifico, la cantidad de veces que ha sido prestado en cada mes 
        //A lo largo de los años (2020-2024), es decir, regresara los 12 meses de cada año
        //Recibe el id del libro como parametro
        [HttpGet]
        [Route("GetPrestamosPorMesPorLibro")]
        public async Task<IActionResult> GetPrestamosPorMesPorLibro(int idLibro)
        {
            var prestamos = await _contexto.DetallesPrestamos
                .Include(detalle => detalle.Libro)
                .Include(detalle => detalle.Prestamo)
                .Where(detalle => detalle.Libro.IdLibro == idLibro && detalle.Prestamo.FechaPrestamo.Year >= 2020
                                                                   && detalle.Prestamo.FechaPrestamo.Year <= 2024)
                .GroupBy(detalle => new { detalle.Prestamo.FechaPrestamo.Year, detalle.Prestamo.FechaPrestamo.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            var prestamosPorMes = new List<object>();

            for (int year = 2020; year <= 2024; year++)
            {
                var meses = new Dictionary<string, int>();
                for (int month = 1; month <= 12; month++)
                {
                    var monthName = new System.Globalization.DateTimeFormatInfo().GetMonthName(month);
                    var prestamoMes = prestamos.FirstOrDefault(p => p.Year == year && p.Month == month);
                    meses[monthName] = prestamoMes?.Count ?? 0;
                }
                //Total de prestamos de cada año
                meses["Total"] = meses.Values.Sum();
                prestamosPorMes.Add(new { Anio = year, Meses = meses });
            }

            var result = new
            {
                IdLibro = idLibro,
                PrestamosPorMes = prestamosPorMes
            };

            return Ok(result);
        }






    }
}
