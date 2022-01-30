using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LALC.Models
{
    
    public class Concepto
    {
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public int ConceptoID { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [DisplayName("Subcategoría")]
        public int SubcategoriaID { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public String Definicion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MinLength(1)]
        public String Titulo { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }

    }
}