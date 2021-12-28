using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LALC_UWP.Models
{
    public class Subcategoria
    {


        public int SubcategoriaID { get; set; }

        public int CategoriaID { get; set; }

        public string Nombre { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        public virtual Categoria Categoria { get; set; }

    }
}
