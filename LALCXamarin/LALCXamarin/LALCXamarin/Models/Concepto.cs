using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LALC_UWP.Models
{
    public class Concepto
    {

        public int ConceptoID { get; set; }

        public int SubcategoriaID { get; set; }

        public string Definicion { get; set; }

        public string Titulo { get; set; }

        public virtual Subcategoria Subcategoria { get; set; }

    }
}
