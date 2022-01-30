using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LALC.Models;
using PagedList;

namespace LALC.BDService
{
    public class GetData
    {
        private LALCDb db = new LALCDb();
        public int getUserID()
        {
            var usuarios = from s in db.Usuario select s;
            String userEmail = HttpContext.Current.User.Identity.Name;
            var usuario = usuarios.Single(s => s.email == userEmail);
            return usuario.UsuarioID;
        }
    }
}