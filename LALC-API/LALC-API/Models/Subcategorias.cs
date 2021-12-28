namespace LALC_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subcategorias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subcategorias()
        {
            Conceptoes = new HashSet<Conceptoes>();
            Practicas = new HashSet<Practicas>();
        }

        [Key]
        public int SubcategoriaID { get; set; }

        public int CategoriaID { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        public virtual Categorias Categorias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conceptoes> Conceptoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Practicas> Practicas { get; set; }
    }
}
