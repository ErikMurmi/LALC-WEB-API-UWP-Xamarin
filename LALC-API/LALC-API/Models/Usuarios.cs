namespace LALC_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            Categorias = new HashSet<Categorias>();
        }

        [Key]
        public int UsuarioID { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        [Required]
        public string nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categorias> Categorias { get; set; }
    }
}
