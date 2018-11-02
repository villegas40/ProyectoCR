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
    public class FotosHabilitacionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/FotosHabilitacions
        public IQueryable<FotosHabilitacion> GetFotosHabilitacion()
        {
            return db.FotosHabilitacion;
        }

        // GET: api/FotosHabilitacions/5
        [Route("api/FotosHabilitacions/{id}")]
        [ResponseType(typeof(FotosHabilitacion))]
        public IHttpActionResult GetFotosHabilitacion(int id)
        {
            FotosHabilitacion fotosHabilitacion = db.FotosHabilitacion.Find(id);
            if (fotosHabilitacion == null)
            {
                return NotFound();
            }

            return Ok(fotosHabilitacion);
        }

        // PUT: api/FotosHabilitacions/5
        [Route("api/FotosHabilitacions/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFotosHabilitacion(int id, FotosHabilitacion fotosHabilitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fotosHabilitacion.fh_id)
            {
                return BadRequest();
            }

            db.Entry(fotosHabilitacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotosHabilitacionExists(id))
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

        // POST: api/FotosHabilitacions
        [ResponseType(typeof(FotosHabilitacion))]
        public IHttpActionResult PostFotosHabilitacion(FotosHabilitacion fotosHabilitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FotosHabilitacion.Add(fotosHabilitacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fotosHabilitacion.fh_id }, fotosHabilitacion);
        }

        // DELETE: api/FotosHabilitacions/5
        [Route("api/FotosHabilitacions/{id}")]
        [ResponseType(typeof(FotosHabilitacion))]
        public IHttpActionResult DeleteFotosHabilitacion(int id)
        {
            FotosHabilitacion fotosHabilitacion = db.FotosHabilitacion.Find(id);
            if (fotosHabilitacion == null)
            {
                return NotFound();
            }

            db.FotosHabilitacion.Remove(fotosHabilitacion);
            db.SaveChanges();

            return Ok(fotosHabilitacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FotosHabilitacionExists(int id)
        {
            return db.FotosHabilitacion.Count(e => e.fh_id == id) > 0;
        }
    }
}