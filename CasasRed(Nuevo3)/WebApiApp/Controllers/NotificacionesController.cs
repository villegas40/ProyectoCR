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
    public class NotificacionesController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Notificaciones
        public IQueryable<Notificaciones> GetNotificaciones()
        {
            return db.Notificaciones;
        }

        // GET: api/Notificaciones/5
        [Route("api/Notificaciones/{id}")]
        [ResponseType(typeof(Notificaciones))]
        public IHttpActionResult GetNotificaciones(int id)
        {
            Notificaciones notificaciones = db.Notificaciones.Find(id);
            if (notificaciones == null)
            {
                return NotFound();
            }

            return Ok(notificaciones);
        }

        // PUT: api/Notificaciones/5
        [Route("api/Notificaciones/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotificaciones(int id, Notificaciones notificaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notificaciones.Id)
            {
                return BadRequest();
            }

            db.Entry(notificaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacionesExists(id))
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

        // POST: api/Notificaciones
        [ResponseType(typeof(Notificaciones))]
        public IHttpActionResult PostNotificaciones(Notificaciones notificaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notificaciones.Add(notificaciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notificaciones.Id }, notificaciones);
        }

        // DELETE: api/Notificaciones/5
        [Route("api/Notificaciones/{id}")]
        [ResponseType(typeof(Notificaciones))]
        public IHttpActionResult DeleteNotificaciones(int id)
        {
            Notificaciones notificaciones = db.Notificaciones.Find(id);
            if (notificaciones == null)
            {
                return NotFound();
            }

            db.Notificaciones.Remove(notificaciones);
            db.SaveChanges();

            return Ok(notificaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificacionesExists(int id)
        {
            return db.Notificaciones.Count(e => e.Id == id) > 0;
        }
    }
}