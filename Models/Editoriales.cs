using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard_API_BiblioPro.Models
{


    [Table("editoriales")]
    public class Editoriales
    {
        [Key]
        [Column("id_editorial")]
            public int IdEditorial { get; set; }
        
        [Column("nombre_editorial")]
            public string NombreEditorial { get; set; }
    }
}
