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
    public class UbicacionesController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Ubicaciones
        public IQueryable<Ubicaciones> GetUbicaciones()
        {
            return db.Ubicaciones;
        }

        // GET: api/Ubicaciones/5
        [ResponseType(typeof(Ubicaciones))]
        public IHttpActionResult GetUbicaciones(int id)
        {
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return NotFound();
            }

            return Ok(ubicaciones);
        }

        // PUT: api/Ubicaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUbicaciones(int id, Ubicaciones ubicaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ubicaciones.ubi_id)
            {
                return BadRequest();
            }

            db.Entry(ubicaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbicacionesExists(id))
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

        // POST: api/Ubicaciones
        [ResponseType(typeof(Ubicaciones))]
        public IHttpActionResult PostUbicaciones(Ubicaciones ubicaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ubicaciones.Add(ubicaciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ubicaciones.ubi_id }, ubicaciones);
        }

        // DELETE: api/Ubicaciones/5
        [ResponseType(typeof(Ubicaciones))]
        public IHttpActionResult DeleteUbicaciones(int id)
        {
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return NotFound();
            }

            db.Ubicaciones.Remove(ubicaciones);
            db.SaveChanges();

            return Ok(ubicaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UbicacionesExists(int id)
        {
            return db.Ubicaciones.Count(e => e.ubi_id == id) > 0;
        }
    }
}