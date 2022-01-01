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
    public class PracticasController : ApiController
    {
        private ModeloLALC db = new ModeloLALC();

        // GET: api/Practicas
        public IQueryable<Practica> GetPracticas()
        {
            return db.Practicas;
        }

        // GET: api/Practicas/5
        [ResponseType(typeof(Practica))]
        public IHttpActionResult GetPractica(int id)
        {
            Practica practica = db.Practicas.Find(id);
            if (practica == null)
            {
                return NotFound();
            }

            return Ok(practica);
        }

        // PUT: api/Practicas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPractica(int id, Practica practica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != practica.PracticaID)
            {
                return BadRequest();
            }

            db.Entry(practica).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PracticaExists(id))
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

        // POST: api/Practicas
        [ResponseType(typeof(Practica))]
        public IHttpActionResult PostPractica(Practica practica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Practicas.Add(practica);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = practica.PracticaID }, practica);
        }

        // DELETE: api/Practicas/5
        [ResponseType(typeof(Practica))]
        public IHttpActionResult DeletePractica(int id)
        {
            Practica practica = db.Practicas.Find(id);
            if (practica == null)
            {
                return NotFound();
            }

            db.Practicas.Remove(practica);
            db.SaveChanges();

            return Ok(practica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PracticaExists(int id)
        {
            return db.Practicas.Count(e => e.PracticaID == id) > 0;
        }
    }
}