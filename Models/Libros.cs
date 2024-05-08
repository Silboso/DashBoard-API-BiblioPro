using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DashBoard_API_BiblioPro.Models
{
    

     [Table("libros")]
    public class Libros
    {
        [Key]
        [Column("id_libro")]
        public int IdLibro { get; set; }
        
        [Column("titulo_libro")]
        public string TituloLibro { get; set; }
        
        [Column("id_categoria")]
        public int IdCategoria { get; set; }
        
        [Column("no_libros")]
        public int NoLibros { get; set; }
        
        [Column("id_editorial")]
        public int IdEditorial { get; set; }
        
        [ForeignKey("IdCategoria")]
        public Categorias Categoria { get; set; }
        
        [ForeignKey("IdEditorial")]
        public Editoriales Editorial { get; set; }
    }
}
