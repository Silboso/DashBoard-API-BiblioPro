using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard_API_BiblioPro.Models
{
  
    [Table("penalizaciones")]

    public class Penalizaciones
    {
        [Key]
        [Column("id_penalizacion")]
        public int IdPenalizacion { get; set; }
        
        [ForeignKey("IdUsuario")]
        public Usuarios Usuario { get; set; }
        
        
        [Column("observaciones")]
        public string Observaciones { get; set; }
        
        
    }
}
