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
    public class GestionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Gestions
        public IQueryable<Gestion> GetGestion()
        {
            return db.Gestion;
        }

        // GET: api/Gestions/5
        [ResponseType(typeof(Gestion))]
        public IHttpActionResult GetGestion(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return NotFound();
            }

            return Ok(gestion);
        }

        // PUT: api/Gestions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGestion(int id, Gestion gestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gestion.Id)
            {
                return BadRequest();
            }

            db.Entry(gestion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GestionExists(id))
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

        // POST: api/Gestions
        [ResponseType(typeof(Gestion))]
        public IHttpActionResult PostGestion(Gestion gestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gestion.Add(gestion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gestion.Id }, gestion);
        }

        // DELETE: api/Gestions/5
        [ResponseType(typeof(Gestion))]
        public IHttpActionResult DeleteGestion(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return NotFound();
            }

            db.Gestion.Remove(gestion);
            db.SaveChanges();

            return Ok(gestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GestionExists(int id)
        {
            return db.Gestion.Count(e => e.Id == id) > 0;
        }
    }
}