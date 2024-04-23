using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DashBoard_API_BiblioPro.Models
{
    
    [Table("detalles_prestamo")]
    public class DetallesPrestamos
    {
        [Key]
        [Column("id_detalle_prestamo")]
        public int IdDetallePrestamo { get; set; }

        [Column("id_prestamo")]
        public int IdPrestamo { get; set; }
        
        [ForeignKey("IdEjemplar")]
        public Libros IdLibro { get; set; }
    }
}
