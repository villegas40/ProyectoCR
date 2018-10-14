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
    public class ComisionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Comisions
        public IQueryable<Comision> GetComision()
        {
            return db.Comision;
        }

        // GET: api/Comisions/5
        [Route("api/Comisions/{id}")]
        [ResponseType(typeof(Comision))]
        public IHttpActionResult GetComision(int id)
        {
            Comision comision = db.Comision.Find(id);
            if (comision == null)
            {
                return NotFound();
            }

            return Ok(comision);
        }

        // PUT: api/Comisions/5
        [Route("api/Comisions/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComision(int id, Comision comision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comision.Id)
            {
                return BadRequest();
            }

            db.Entry(comision).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComisionExists(id))
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

        // POST: api/Comisions
        [ResponseType(typeof(Comision))]
        public IHttpActionResult PostComision(Comision comision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comision.Add(comision);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comision.Id }, comision);
        }

        // DELETE: api/Comisions/5
        [ResponseType(typeof(Comision))]
        public IHttpActionResult DeleteComision(int id)
        {
            Comision comision = db.Comision.Find(id);
            if (comision == null)
            {
                return NotFound();
            }

            db.Comision.Remove(comision);
            db.SaveChanges();

            return Ok(comision);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComisionExists(int id)
        {
            return db.Comision.Count(e => e.Id == id) > 0;
        }
    }
}