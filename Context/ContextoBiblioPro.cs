using DashBoard_API_BiblioPro.Models;
using Microsoft.EntityFrameworkCore;

namespace DashBoard_API_BiblioPro.Context
{
    public class ContextoBiblioPro : DbContext
    {
        public DbSet<Categorias> Categorias { get; set; }

        public DbSet<DetallesPrestamos> DetallesPrestamos { get; set; }

        public DbSet<Editoriales> Editoriales { get; set; }

        public DbSet<Estados> Estados { get; set; }

        public DbSet<Libros> Libros { get; set; }

        public DbSet<Penalizaciones> Penalizaciones { get; set; }

        public DbSet<Prestamos> Prestamos { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

        public ContextoBiblioPro(DbContextOptions<ContextoBiblioPro> options) : base(options)
        {
        }
        
        
    }
}
