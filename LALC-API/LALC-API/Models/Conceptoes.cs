namespace LALC_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Conceptoes
    {
        [Key]
        public int ConceptoID { get; set; }

        public int SubcategoriaID { get; set; }

        [Required]
        public string Definicion { get; set; }

        [Required]
        public string Titulo { get; set; }

        public virtual Subcategorias Subcategorias { get; set; }
    }
}
