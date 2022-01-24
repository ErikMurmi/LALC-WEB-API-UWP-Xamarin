using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LALC_UWP.Models
{
    public class Practica
    {
        public int PracticaID { get; set; }

        //public int UsuarioID { get; set; }

        public int SubcategoriaID { get; set; }

        public int CantidadConceptos { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Subcategoria Subcategoria { get; set; }
        
        //public virtual Usuario Usuario { get; set; }

    }
}
