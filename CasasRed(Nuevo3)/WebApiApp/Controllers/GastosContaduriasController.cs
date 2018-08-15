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
    public class GastosContaduriasController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/GastosContadurias
        public IQueryable<GastosContaduria> GetGastosContaduria()
        {
            return db.GastosContaduria;
        }

        // GET: api/GastosContadurias/5
        [ResponseType(typeof(GastosContaduria))]
        public IHttpActionResult GetGastosContaduria(int id)
        {
            GastosContaduria gastosContaduria = db.GastosContaduria.Find(id);
            if (gastosContaduria == null)
            {
                return NotFound();
            }

            return Ok(gastosContaduria);
        }

        // PUT: api/GastosContadurias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGastosContaduria(int id, GastosContaduria gastosContaduria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gastosContaduria.Id)
            {
                return BadRequest();
            }

            db.Entry(gastosContaduria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastosContaduriaExists(id))
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

        // POST: api/GastosContadurias
        [ResponseType(typeof(GastosContaduria))]
        public IHttpActionResult PostGastosContaduria(GastosContaduria gastosContaduria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GastosContaduria.Add(gastosContaduria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gastosContaduria.Id }, gastosContaduria);
        }

        // DELETE: api/GastosContadurias/5
        [ResponseType(typeof(GastosContaduria))]
        public IHttpActionResult DeleteGastosContaduria(int id)
        {
            GastosContaduria gastosContaduria = db.GastosContaduria.Find(id);
            if (gastosContaduria == null)
            {
                return NotFound();
            }

            db.GastosContaduria.Remove(gastosContaduria);
            db.SaveChanges();

            return Ok(gastosContaduria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GastosContaduriaExists(int id)
        {
            return db.GastosContaduria.Count(e => e.Id == id) > 0;
        }
    }
}