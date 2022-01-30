using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace LALC.Models
{
    public class Subcategoria
    {
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public int SubcategoriaID { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [DisplayName("Categoria Padre")]
        public int CategoriaID { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MinLength(1)]
        public String Nombre { get; set; }
        public String Color { get; set; }
        public String Descripcion { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Concepto> Concepto { get; set; }
    }
}