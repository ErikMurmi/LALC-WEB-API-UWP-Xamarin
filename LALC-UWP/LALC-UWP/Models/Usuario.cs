using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LALC_UWP.Models
{
    public class Usuario
    {

        public int UsuarioID { get; set; }

        public string email { get; set; }

        public string password { get; set; }
        public string nombre { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }



    }
}
