using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard_API_BiblioPro.Models
{
    //    CREATE TABLE Categorias(
    //    id_categoria INT PRIMARY KEY,
    //    nombre_categoria VARCHAR(255)
    //);

    [Table("categorias")]
    public class Categorias
    {
        [Key]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }
        
        [Column("nombre_categoria")]
        public string NombreCategoria { get; set; }
    }
}
