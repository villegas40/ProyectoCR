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
    public class HistorialAsignacionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/HistorialAsignacions
        public IQueryable<HistorialAsignacion> GetHistorialAsignacion()
        {
            return db.HistorialAsignacion;
        }

        // GET: api/HistorialAsignacions/5
        [Route("api/HistorialAsignacions/{id}")]
        [ResponseType(typeof(HistorialAsignacion))]
        public IHttpActionResult GetHistorialAsignacion(int id)
        {
            HistorialAsignacion historialAsignacion = db.HistorialAsignacion.Find(id);
            if (historialAsignacion == null)
            {
                return NotFound();
            }

            return Ok(historialAsignacion);
        }

        // PUT: api/HistorialAsignacions/5
        [Route("api/HistorialAsignacions/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHistorialAsignacion(int id, HistorialAsignacion historialAsignacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historialAsignacion.ha_id)
            {
                return BadRequest();
            }

            db.Entry(historialAsignacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialAsignacionExists(id))
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

        // POST: api/HistorialAsignacions
        [ResponseType(typeof(HistorialAsignacion))]
        public IHttpActionResult PostHistorialAsignacion(HistorialAsignacion historialAsignacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HistorialAsignacion.Add(historialAsignacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = historialAsignacion.ha_id }, historialAsignacion);
        }

        // DELETE: api/HistorialAsignacions/5
        [Route("api/HistorialAsignacions/{id}")]
        [ResponseType(typeof(HistorialAsignacion))]
        public IHttpActionResult DeleteHistorialAsignacion(int id)
        {
            HistorialAsignacion historialAsignacion = db.HistorialAsignacion.Find(id);
            if (historialAsignacion == null)
            {
                return NotFound();
            }

            db.HistorialAsignacion.Remove(historialAsignacion);
            db.SaveChanges();

            return Ok(historialAsignacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HistorialAsignacionExists(int id)
        {
            return db.HistorialAsignacion.Count(e => e.ha_id == id) > 0;
        }
    }
}