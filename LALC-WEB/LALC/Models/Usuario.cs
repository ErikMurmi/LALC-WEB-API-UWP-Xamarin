using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LALC.Models;

namespace LALC.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail no es válido")]
        public String email{ get; set; }
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Debe contener almenos 8 carácteres")]
        public String password{ get; set; }
        [Required]
        public String nombre{ get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }

        //public virtual ICollection<Practica> Practicas { get; set; }

        public bool Autenticar()
        {
            LALCDb db = new LALCDb();
            return db.Usuario.Where(u => u.email == this.email && u.password == this.password).FirstOrDefault() != null;
        }
    }
}