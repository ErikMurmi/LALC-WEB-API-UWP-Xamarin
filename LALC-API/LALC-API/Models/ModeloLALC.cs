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
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Conceptoes> Conceptoes { get; set; }
        public virtual DbSet<Practicas> Practicas { get; set; }
        public virtual DbSet<Subcategorias> Subcategorias { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
