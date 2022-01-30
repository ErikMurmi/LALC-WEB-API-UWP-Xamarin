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
using PagedList.Mvc;

namespace LALC.Controllers
{
    [Authorize]
    public class SubcategoriasController : Controller
    {
        private LALCDb db = new LALCDb();
        private static int createId = 1;
        // GET: Subcategorias
        public ActionResult Index(String TituloS)
        {
            var Subcategoria = from s in db.Subcategoria select s;
            if (!String.IsNullOrEmpty(TituloS))
            {
                Subcategoria = Subcategoria.Where(s => s.Nombre.Contains(TituloS));
                return View(Subcategoria.ToList());
            }
            return View(db.Subcategoria.ToList());
        }


        public ActionResult SpecificSubcategories(int? id, String TituloS,int? pagina)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            createId = (int)id;
            var subcategoria = from s in db.Subcategoria select s;
            List<Subcategoria> subcategorias = new List<Subcategoria>();
            subcategoria = subcategoria.Where(s => s.CategoriaID == id);
            foreach (var ct in subcategoria.ToList())
            {   
                subcategorias.Add(ct);
            }
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            if (!String.IsNullOrEmpty(TituloS))
            {
                subcategoria = subcategoria.Where(s => s.Nombre.Contains(TituloS));
                return View(subcategoria.ToList().ToPagedList(pagina ?? 1, 12));
            }
            subcategorias = subcategorias.OrderBy(ct => ct.Nombre).ToList();
            return View(subcategorias.ToPagedList(pagina ?? 1, 12));
        }

        // GET: Subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        public static int getCreateId()
        {
            return createId;
        }

        // GET: Subcategorias/Create
        public ActionResult Create(String CategoriaID)
        {
            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Nombre");
            return View();
        }

        // POST: Subcategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubcategoriaID,CategoriaID,Nombre,Color,Descripcion")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Subcategoria.Add(subcategoria);
                db.SaveChanges();
                return RedirectToAction("SpecificSubcategories", "Subcategorias", new { id = subcategoria.CategoriaID });
            }

            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Nombre", subcategoria.CategoriaID);
            return View(subcategoria);
        }

        // GET: Subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Nombre", subcategoria.CategoriaID);
            return View(subcategoria);
        }

        // POST: Subcategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubcategoriaID,CategoriaID,Nombre,Color,Descripcion")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SpecificSubcategories", new { id = subcategoria.CategoriaID });
            }
            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Nombre", subcategoria.CategoriaID);
            return View(subcategoria);
        }

        // GET: Subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            db.Subcategoria.Remove(subcategoria);
            db.SaveChanges();
            return RedirectToAction("SpecificSubcategories", new { id= subcategoria.CategoriaID});
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
