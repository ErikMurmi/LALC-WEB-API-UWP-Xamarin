using LALC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace LALC.Controllers
{
    public class AuthController : Controller
    {
        private LALCDb db = new LALCDb();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario, string ReturnUrl)
        {
            if (IsValid(usuario))
            {
                FormsAuthentication.SetAuthCookie(usuario.email, false);
                if(ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index","Home");
            }
            TempData["mensaje"] = "Constraseña o correo incorrectos";
            return View(usuario);
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar([Bind(Include = "UsuarioID,email,password,nombre")] Usuario usuario, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(usuario.email, false);
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            TempData["mensaje"] = "Campos ingresados no son válidos";
            return View("Registrar");
        }

        private bool IsValid(Usuario usuario)
        {
            return usuario.Autenticar();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }


}