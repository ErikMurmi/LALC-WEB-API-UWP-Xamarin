using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LALC.Models
{
    public class Categoria
    {
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public int CategoriaID { get; set; }

        public int UsuarioID { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MinLength(1)]
        public String Nombre { get; set; }
        public String Color { get; set; }
        public String Descripcion { get; set; }
        public Boolean esPrioritaria { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Subcategoria> Subcategoria { get; set; }
    }
}