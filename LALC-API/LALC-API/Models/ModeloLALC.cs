using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LALC_API.Models
{
    public partial class ModeloLALC : DbContext
    {
        public ModeloLALC()
            : base("name=ModeloLALC")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Concepto> Conceptoes { get; set; }
        public virtual DbSet<Practica> Practicas { get; set; }
        public virtual DbSet<Subcategoria> Subcategorias { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
