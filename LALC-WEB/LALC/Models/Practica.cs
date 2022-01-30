using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LALC.Models
{
    public class Practica
    {
        [Key]
        public int PracticaID { get; set; }
        //public int UsuarioID{ get; set; }
        public int SubcategoriaID { get; set; }
        public int CantidadConceptos { get; set; }

        [Display(Name = "Fecha")]
        //[DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        
        public virtual Subcategoria Subcategoria { get; set; }

        //public virtual Usuario Usuario { get; set; }


    }
}
