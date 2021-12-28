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
        public IQueryable<Practicas> GetPracticas()
        {
            return db.Practicas;
        }

        // GET: api/Practicas/5
        [ResponseType(typeof(Practicas))]
        public IHttpActionResult GetPracticas(int id)
        {
            Practicas practicas = db.Practicas.Find(id);
            if (practicas == null)
            {
                return NotFound();
            }

            return Ok(practicas);
        }

        // PUT: api/Practicas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPracticas(int id, Practicas practicas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != practicas.PracticaID)
            {
                return BadRequest();
            }

            db.Entry(practicas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PracticasExists(id))
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
        [ResponseType(typeof(Practicas))]
        public IHttpActionResult PostPracticas(Practicas practicas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Practicas.Add(practicas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = practicas.PracticaID }, practicas);
        }

        // DELETE: api/Practicas/5
        [ResponseType(typeof(Practicas))]
        public IHttpActionResult DeletePracticas(int id)
        {
            Practicas practicas = db.Practicas.Find(id);
            if (practicas == null)
            {
                return NotFound();
            }

            db.Practicas.Remove(practicas);
            db.SaveChanges();

            return Ok(practicas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PracticasExists(int id)
        {
            return db.Practicas.Count(e => e.PracticaID == id) > 0;
        }
    }
}