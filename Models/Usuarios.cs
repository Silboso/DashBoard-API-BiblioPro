
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard_API_BiblioPro.Models
{
    

    [Table("usuarios")]
    public class Usuarios
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }
        
        [Column("nombre_usuario")]
        public string NombreUsuario { get; set; }
        
        [Column("apellido1_usuario")]
        public string Apellido1Usuario { get; set; }
        
        [Column("apellido2_usuario")]
        public string Apellido2Usuario { get; set; }
        
        [Column("id_estado")]
        public int IdEstado { get; set; }
        
        [ForeignKey("IdEstado")]
        public Estados Estado { get; set; }
    }
}
