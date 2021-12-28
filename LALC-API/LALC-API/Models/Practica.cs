namespace LALC_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Practica
    {
        [Key]
        public int PracticaID { get; set; }

        public int SubcategoriaID { get; set; }

        public int CantidadConceptos { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Subcategoria Subcategoria { get; set; }
    }
}
