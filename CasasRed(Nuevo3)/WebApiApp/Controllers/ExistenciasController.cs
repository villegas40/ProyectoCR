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
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class ExistenciasController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Existencias
        public IQueryable<Existencias> GetExistencias()
        {
            return db.Existencias;
        }

        // GET: api/Existencias/5
        [ResponseType(typeof(Existencias))]
        public IHttpActionResult GetExistencias(int id)
        {
            Existencias existencias = db.Existencias.Find(id);
            if (existencias == null)
            {
                return NotFound();
            }

            return Ok(existencias);
        }

        // PUT: api/Existencias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExistencias(int id, Existencias existencias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != existencias.Id)
            {
                return BadRequest();
            }

            db.Entry(existencias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistenciasExists(id))
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

        // POST: api/Existencias
        [ResponseType(typeof(Existencias))]
        public IHttpActionResult PostExistencias(Existencias existencias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Existencias.Add(existencias);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = existencias.Id }, existencias);
        }

        // DELETE: api/Existencias/5
        [ResponseType(typeof(Existencias))]
        public IHttpActionResult DeleteExistencias(int id)
        {
            Existencias existencias = db.Existencias.Find(id);
            if (existencias == null)
            {
                return NotFound();
            }

            db.Existencias.Remove(existencias);
            db.SaveChanges();

            return Ok(existencias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExistenciasExists(int id)
        {
            return db.Existencias.Count(e => e.Id == id) > 0;
        }
    }
}