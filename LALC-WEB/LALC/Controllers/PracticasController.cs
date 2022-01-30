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

namespace LALC.Controllers
{
    [Authorize]
    public class PracticasController : Controller
    {
        public static int total = -1;
        public static int subcategoriaId;
        private LALCDb db = new LALCDb();
        private GetData data = new GetData();

        // GET: Practicas
        public ActionResult Index()
        {
            var practicas = from p in db.Practicas select p;
            int userid = data.getUserID();
            practicas = practicas.Where(p => p.Subcategoria.Categoria.UsuarioID == userid);
            return View(practicas.ToList());
        }

        // GET: Practicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practica practica = db.Practicas.Find(id);
            if (practica == null)
            {
                return HttpNotFound();
            }
            return View(practica);
        }

        // GET: Practicas/Create
        public ActionResult Create()
        {
            return View();
        }

        public static int AumentarTotal()
        {
            total++;
            return 1;
        }

        
        public ActionResult agregarPractica()
        {
            Practica p = new Practica();
            p.CantidadConceptos = total;
            p.Fecha = DateTime.Now;
            p.SubcategoriaID = LALC.Controllers.ConceptoesController.subcategoriaID;
            //p.UsuarioID = data.getUserID();
            Create(p);
            total = -1;
            return RedirectToAction("Index","Practicas");
        }

        public ActionResult Practice(int? id)
        {
            Random a = new Random();
            var concepto = from s in db.Concepto select s;
            concepto = concepto.Where(s => s.SubcategoriaID == id);
            if (concepto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Concepto> conceptos = concepto.ToList();
            int v = a.Next(0, conceptos.Count);
            Concepto c_random = conceptos[v];
            AumentarTotal();
            return View(c_random);
        }


        // POST: Practicas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PracticaID,SubcategoriaID,Correctos,Incorrectos,Fecha")] Practica practica)
        {
            if (ModelState.IsValid)
            {
                db.Practicas.Add(practica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(practica);
        }

        // GET: Practicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practica practica = db.Practicas.Find(id);
            if (practica == null)
            {
                return HttpNotFound();
            }
            return View(practica);
        }

        
        // POST: Practicas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PracticaID,Correctos,Incorrectos,Fecha")] Practica practica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(practica);
        }

        // GET: Practicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practica practica = db.Practicas.Find(id);
            if (practica == null)
            {
                return HttpNotFound();
            }
            return View(practica);
        }

        // POST: Practicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Practica practica = db.Practicas.Find(id);
            db.Practicas.Remove(practica);
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
