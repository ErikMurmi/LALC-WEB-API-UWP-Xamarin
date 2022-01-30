using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LALC.BDService;
using LALC.Models;
using PagedList;
using PagedList.Mvc;

namespace LALC.Controllers
{

    [Authorize]
    public class CategoriasController : Controller
    {
        private LALCDb db = new LALCDb();
        public static int id=0;
        private GetData data = new GetData();
        // GET: Categorias
        public ActionResult Index(String Titulo,int? pagina)
        {
            var categoria = from s in db.Categoria select s;
            List<Categoria> categorias = new List<Categoria>();
            int userid = data.getUserID();
            categoria = categoria.Where(s => s.UsuarioID == userid);
            if (!String.IsNullOrEmpty(Titulo))
            {
                categoria = categoria.Where(s => s.Nombre.Contains(Titulo));
                return View(categoria.ToList().ToPagedList(pagina ?? 1, 12));
            }
            foreach (var ct in categoria.ToList())
            {
                if (!ct.esPrioritaria)
                {
                    categorias.Add(ct);
                }
            }
            categorias = categorias.OrderBy(ct=>ct.Nombre).ToList();
            return View(categorias.ToPagedList(pagina ?? 1, 12));
        }

        //Saave data
        public ActionResult SaveData()
        {
            LALCDb db = new LALCDb();
            var categoria = from s in db.Categoria select s;
            List<Categoria> categorias = categoria.ToList();
            if(categorias.Count==0)
            {
                Categoria Lengua = new Categoria();
                Lengua.Nombre = "Lenguaje";
                Lengua.Descripcion = "Son letras";
                Lengua.Color = "#8a2e28";
                Create(Lengua);
                Categoria Mate = new Categoria();
                Mate.Nombre = "Matematicas";
                Mate.Descripcion = "Son numeros";
                Mate.Color = "#1a4a61";
                Create(Mate);

            }
            return RedirectToAction("Index","Home");
        }


        public static List<Categoria> getPrioritarias()
        {
            LALCDb db = new LALCDb();
            GetData data = new GetData();
            var categoria = from s in db.Categoria select s;
            int userid = data.getUserID();
            categoria = categoria.Where(s => s.UsuarioID == userid);
            List<Categoria> cat = categoria.ToList();
            List<Categoria> prioritarias = new List<Categoria>();
            foreach (var ct in cat)
            {
                if (ct.esPrioritaria)
                {
                    prioritarias.Add(ct);
                }
            }
            prioritarias = prioritarias.OrderBy(c => c.Nombre).ToList();
            return prioritarias;
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoriaID,UsuarioID,Nombre,Color,Descripcion,esPrioritaria")] Categoria categoria)
        {
            /*var usuarios = from s in db.Usuario select s;
            String userid = HttpContext.User.Identity.Name;
            var usuario = usuarios.Single(s => s.email == userid);*/
            if (ModelState.IsValid)
            {
                categoria.UsuarioID = data.getUserID();
                db.Categoria.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaID,Nombre,Color,Descripcion,esPrioritaria")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            db.Categoria.Remove(categoria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
