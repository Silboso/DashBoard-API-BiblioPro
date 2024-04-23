using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard_API_BiblioPro.Models
{
   
    [Table("prestamos")]
    public class Prestamos
    {
        [Key]
        [Column("id_prestamo")]
        public int IdPrestamo { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("fecha_prestamo")]
        public DateTime FechaPrestamo { get; set; }

        [Column("fecha_entrega")]
        public DateTime FechaEntrega { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }
    }
}
