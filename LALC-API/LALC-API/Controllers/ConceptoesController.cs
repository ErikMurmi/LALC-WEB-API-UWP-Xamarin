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
    public class ConceptoesController : ApiController
    {
        private ModeloLALC db = new ModeloLALC();

        // GET: api/Conceptoes
        public IQueryable<Concepto> GetConceptoes()
        {
            return db.Conceptoes;
        }

        // GET: api/Conceptoes/5
        [ResponseType(typeof(Concepto))]
        public IHttpActionResult GetConceptoes(int id)
        {
            Concepto conceptoes = db.Conceptoes.Find(id);
            if (conceptoes == null)
            {
                return NotFound();
            }

            return Ok(conceptoes);
        }

        // PUT: api/Conceptoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConceptoes(int id, Concepto conceptoes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conceptoes.ConceptoID)
            {
                return BadRequest();
            }

            db.Entry(conceptoes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConceptoesExists(id))
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

        // POST: api/Conceptoes
        [ResponseType(typeof(Concepto))]
        public IHttpActionResult PostConceptoes(Concepto conceptoes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Conceptoes.Add(conceptoes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = conceptoes.ConceptoID }, conceptoes);
        }

        // DELETE: api/Conceptoes/5
        [ResponseType(typeof(Concepto))]
        public IHttpActionResult DeleteConceptoes(int id)
        {
            Concepto conceptoes = db.Conceptoes.Find(id);
            if (conceptoes == null)
            {
                return NotFound();
            }

            db.Conceptoes.Remove(conceptoes);
            db.SaveChanges();

            return Ok(conceptoes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConceptoesExists(int id)
        {
            return db.Conceptoes.Count(e => e.ConceptoID == id) > 0;
        }
    }
}