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
    public class CategoriasController : ApiController
    {
        private ModeloLALC db = new ModeloLALC();

        // GET: api/Categorias
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult GetCategorias(int id)
        {
            Categoria categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return NotFound();
            }

            return Ok(categorias);
        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategorias(int id, Categoria categorias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorias.CategoriaID)
            {
                return BadRequest();
            }

            db.Entry(categorias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasExists(id))
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

        // POST: api/Categorias
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult PostCategorias(Categoria categorias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categorias);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categorias.CategoriaID }, categorias);
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategorias(int id)
        {
            Categoria categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categorias);
            db.SaveChanges();

            return Ok(categorias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriasExists(int id)
        {
            return db.Categorias.Count(e => e.CategoriaID == id) > 0;
        }
    }
}