using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LALC_API.Models;

namespace LALC_API.Controllers
{
    public class SubcategoriasController : ApiController
    {
        private ModeloLALC db = new ModeloLALC();

        // GET: api/Subcategorias
        public IQueryable<Subcategoria> GetSubcategorias()
        {
            return db.Subcategorias;
        }

        // GET: api/Subcategorias/5
        [ResponseType(typeof(Subcategoria))]
        public IHttpActionResult GetSubcategorias(int id)
        {
            Subcategoria subcategorias = db.Subcategorias.Find(id);
            if (subcategorias == null)
            {
                return NotFound();
            }

            return Ok(subcategorias);
        }

        // PUT: api/Subcategorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubcategorias(int id, Subcategoria subcategorias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subcategorias.SubcategoriaID)
            {
                return BadRequest();
            }

            db.Entry(subcategorias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoriasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Subcategorias
        [ResponseType(typeof(Subcategoria))]
        public IHttpActionResult PostSubcategorias(Subcategoria subcategorias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subcategorias.Add(subcategorias);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subcategorias.SubcategoriaID }, subcategorias);
        }

        // DELETE: api/Subcategorias/5
        [ResponseType(typeof(Subcategoria))]
        public IHttpActionResult DeleteSubcategorias(int id)
        {
            Subcategoria subcategorias = db.Subcategorias.Find(id);
            if (subcategorias == null)
            {
                return NotFound();
            }

            db.Subcategorias.Remove(subcategorias);
            db.SaveChanges();

            return Ok(subcategorias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubcategoriasExists(int id)
        {
            return db.Subcategorias.Count(e => e.SubcategoriaID == id) > 0;
        }
    }
}