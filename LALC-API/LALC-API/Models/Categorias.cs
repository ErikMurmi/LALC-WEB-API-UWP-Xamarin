namespace LALC_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categorias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categorias()
        {
            Subcategorias = new HashSet<Subcategorias>();
        }

        [Key]
        public int CategoriaID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        public bool esPrioritaria { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subcategorias> Subcategorias { get; set; }
    }
}
