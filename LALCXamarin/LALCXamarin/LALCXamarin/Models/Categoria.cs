using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LALC_UWP.Models
{
    public class Categoria
    {

        public int CategoriaID { get; set; }

        public int UsuarioID { get; set; }
        public string Nombre { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        public bool esPrioritaria { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Subcategoria> Subcategorias { get; set; }

    }
}
