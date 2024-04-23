using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DashBoard_API_BiblioPro.Models
{
    [Table("estados")]
    public class Estados
    {

        [Key]
        [Column("id_estado")]
        public int IdEstado { get; set; }
        
        [Column("nombre")]
        public string Nombre { get; set; }
        
    }
}
